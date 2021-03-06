using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	//Variables for movement speed, lookspeed and things like how far away you have to be from an object to pick it up, batteries left etc.
	public float movementSpeed;
	public float jumpHeight;
	public float lookSpeed;
	public float objectPickupProximity;
	public float pushForce;
	public float maxTorchPower = 100f;
	public float batteriesLeft = 1f;
	public GameObject collectedObject;
	public GameObject cameraGameObject;
	public GameObject torch;

	//These I kept private because i only need to edit them in this sceript and don't need a reference to them outside of this script.
	private Rigidbody playerBody;
	private float minValue = 0f;
	private float movementSpeedAmplifier = 2f;
	private float raycastValue = 1f;
	private float strengthValue = 5f;
	private float maxThrowPower = 30f;
	private float maxTorchPowerValue = 100f;
	private float torchConsumeValue = 1f;
	private float rotationValue = 180f;
	private ObjectDispenser dispenser;
	private GravityController gravCon;
	private UIScript uiScript;
	private bool playerHasRotated = false;
	private bool isDashing = false;

	//Grabs the scripts that we are going to change values of from inside this script.
	void Start ()
	{
		dispenser = GameObject.Find ("DispenserButton").GetComponent<ObjectDispenser> ();
		gravCon = GameObject.Find ("GravityManager").GetComponent<GravityController> ();
		uiScript = GameObject.Find ("InGameUI").GetComponent<UIScript> ();
		playerBody = GetComponent<Rigidbody> ();
		cameraGameObject = Camera.main.gameObject;

		/*This part is only because of the pause menu. It makes sure you can move if you use the pause
		 * menu to go back to the main menu you can move again after starting. Otherwise you wouldn't
		 * be able to move if you go back to the main menu through this way. Because it doesn't reset
		 * the timescale back to 1. */
		Time.timeScale = 1f;
	}

	void Update ()
	{
		//If the pause menu is active then it will allow you to move your mouse around in the pause screen without being locked.
		if (uiScript.pauseMenu.activeInHierarchy) 
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		} 
		//If the pause menu isn't active then you can do everything as normal.
		else 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = true;

			//Character rotations for left and right for more look controls.
			transform.Rotate (0f, Input.GetAxis ("Mouse X") * lookSpeed, 0f);

			//These are just for running the methods all the time and don't require a previous button press to call the function.
			ConsumeTorchPower ();
			CharacterMovements ();

			//Button to use for jumping and calling the jump method.
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				Jump ();
			}

			//Button to use for grabbing an object. as long as it is held down and you are looking at an object you can pick it up.
			if (Input.GetKeyDown (KeyCode.Mouse1)) 
			{
				GrabObject ();
			}

			//Letting go of the object will throw it depending on your throw power. this lets you throw objects around to suimulate the physics.
			if (Input.GetKeyUp (KeyCode.Mouse1))
			{
				ThrowObject ();
			}

			//Button for interacting with certain objects. Will call the method for doing this.
			if (Input.GetKeyDown (KeyCode.Mouse0)) 
			{
				InteractedWithObject ();
			}

			//Button for changing how much power you use when throwing items. Each press increases it until it gets to max, in which it will set it back to 0.
			if (Input.GetKeyDown (KeyCode.Q)) 
			{
				PowerControl ();
			}

			//Button for turning your torch on or off. however if your power is less than or equal to 0 you cannot turn on the torch.
			if (Input.GetKeyDown (KeyCode.F)) 
			{
				if (maxTorchPower > minValue) 
				{
					TorchOnOff ();
				}
			}

			//If your torch power runs out, then the torch will go off.
			if (maxTorchPower <= minValue) 
			{
				torch.SetActive (false);
			}

			//Button for reloading your torch so you can see again. However if you don't have a battery it wont work.
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				ReloadTorch ();
			}

			//Button for dashing or sprinting. Hold it down to sprint.
			if (Input.GetKeyDown (KeyCode.LeftShift)) 
			{
				isDashing = true;
				Dash ();
			}

			//Let go of this button to stop sprinting and move slower again.
			if (Input.GetKeyUp (KeyCode.LeftShift)) 
			{
				isDashing = false;
				Dash ();
			}
		}
	}

	//This is what lets you dash. If isdashing is true then it speeds you up and vice versa if its false.
	void Dash()
	{
		if (isDashing == true) 
		{
			movementSpeed = movementSpeed * movementSpeedAmplifier;
		}

		else if (isDashing == false) 
		{
			movementSpeed = movementSpeed / movementSpeedAmplifier;
		}
	}

	/*This is the basic movement of the character. The only reason i'm not using a character controller is because I couldnt get gravity 
	to work with it since im making my own. Also using a rigidbody based character controller makes the player move way too fast in terms
	of going forward and will always speed up. So for example you'd hold down the forwrad button and you would slowly get faster and faster 
	until you end up glitching through walls. */
	void CharacterMovements()
	{
		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime, 0f, Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime));
	}

	//This adds force to the player to simulate jumping when you press the spacebar. You have to be on the ground to be able to jump.
	void Jump()
	{ 
		if (PlayerIsGrounded())
		{
			playerBody.AddRelativeForce (Vector3.up * jumpHeight, ForceMode.Impulse);
		}
	}

	//This is a custom method for checking if you are touching the ground. It sets a raycast to check and then returns if it hit something just outside the scale of the player.
	bool PlayerIsGrounded()
	{
		return (Physics.Raycast (transform.position, -transform.up, raycastValue));
	}

	/*This does a raycast to see what it it hit and if it hit an object using the right click then the object it it, if it has a rigidbody, will set it to be kinematic so you 
	//can pick it up and it wont drop unless you let go of right click. It works by parenting it to the camera so it moves with it while you are holding it. */
	void GrabObject()
	{
		RaycastHit forceHit;
		Ray forceOnClick = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (forceOnClick, out forceHit)) 
		{
			if (forceHit.rigidbody.isKinematic == false)
			{
				if (Vector3.Distance (transform.position, forceHit.transform.position) <= objectPickupProximity) 
				{
					collectedObject = forceHit.rigidbody.gameObject;
					forceHit.rigidbody.isKinematic = true;
					forceHit.rigidbody.gameObject.transform.parent = cameraGameObject.transform;
				}
			}
		}
	}

	//This throws the object and no longer parents it to you and turns it to not be kinematic anymore so it can be affected by physics again.
	void ThrowObject()
	{
		if (collectedObject != null) 
		{
			collectedObject.transform.parent = null;
			collectedObject.GetComponent<Rigidbody> ().isKinematic = false;
			collectedObject.GetComponent<Rigidbody> ().AddForce (cameraGameObject.transform.forward * pushForce, ForceMode.Impulse);
			collectedObject = null;
		}
	}

	//This is the big method that detects what you click with the left mouse button. Each item clicked that cane be interacted with has a different outcome.
	void InteractedWithObject()
	{
		RaycastHit objectInteractedWith;
		Ray objectOnClick = Camera.main.ScreenPointToRay (Input.mousePosition);

		//If the object is dispensor one, which will be the cube shaped one, then it will spawn a cube in front of the dispenser by using its script.
		if (Physics.Raycast (objectOnClick, out objectInteractedWith)) 
		{
			if (objectInteractedWith.transform.name == "DispenserButton") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					if (dispenser.objectWasSpawned == false) 
					{
						dispenser.SpawnRandomObject ();
					}
				}
			}

			/* If the player has clicked what has been called "ominous button" then it will access the gravity script and swap gravity around so you walk on the ceiling.
			 * this affects everything that is affected y physics so if you have a sphere with you when you press it, the sphere will fall to the ceiling wih you. Hovever,
			 * should you press the button, you will rotate so that your "feet" will hit the new floor. */
			if (objectInteractedWith.transform.name == "OminousButton") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					gravCon.gravityIsSwapped = !gravCon.gravityIsSwapped;
					RotateCharacter ();
				}
			}

			if (objectInteractedWith.transform.name == "GravityReduceButton") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					gravCon.gravityIsReduced = !gravCon.gravityIsReduced;
				}
			}
		}
	}

	//This is the method that controls your throw power. it goes up in increments of 5 as i saw that as even. It only goes up to 25 because adding more is too much power.
	void PowerControl()
	{
		pushForce += strengthValue;

		if (pushForce >= maxThrowPower) 
		{
			pushForce = minValue;
		}
	}

	/*This consumes the power in your torch so that you have to be more urgent in what you do in learning how to play and getting around the level. It just adds to the fun at this point.
	 * As long as the torch is active, it will continue to consume power. Power will not go down if the torch is off. */
	void ConsumeTorchPower()
	{
		if (torch.activeInHierarchy) 
		{
			maxTorchPower -= torchConsumeValue * Time.deltaTime;
		}
	}

	/*This method is for turning the torch on or off. This is what allows you to see in the game and only has limited power. Something else for the fun of the game. */
	void TorchOnOff()
	{
		if (torch.activeInHierarchy) 
		{
			torch.SetActive (false);
		} 
		else
		{
			torch.SetActive (true);
		}
	}

	//This is what rotates you for when you press the ominous button. it uses a bool because otherwise you continue to rotate even if you haven't pressed it again. Starts glitching out.
	void RotateCharacter()
	{
		if (playerHasRotated == false)
		{
			playerHasRotated = true;
			transform.RotateAround (transform.position, transform.forward, rotationValue);
			playerHasRotated = false;
		}
	}

	//This is what reloads your torch. you can spam it but its not worth it since once there is no more batteries you cannot reload the torch. it takes away a battery each time you do it.
	void ReloadTorch()
	{
		if (batteriesLeft > minValue) 
		{
			batteriesLeft--;
			maxTorchPower = maxTorchPowerValue;
		}
	}
}

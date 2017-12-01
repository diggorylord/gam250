using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
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

	private Rigidbody playerBody;
	private ObjectDispenser dispenser;
	private LevelChangerButton buttonScript;
	private GravityController gravCon;
	private BatteryCollect battPick;
	private bool playerHasRotated = false;
	private bool isDashing = false;

	void Start ()
	{
		dispenser = GameObject.Find ("Dispenser1").GetComponent<ObjectDispenser> ();
		gravCon = GameObject.Find ("GravityManager").GetComponent<GravityController> ();
		battPick = GameObject.Find ("Battery").GetComponent<BatteryCollect> ();
		playerBody = GetComponent<Rigidbody> ();
		cameraGameObject = Camera.main.gameObject;
	}

	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * lookSpeed, 0f);

		ConsumeTorchPower ();
		CharacterMovements ();

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Jump ();
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) 
		{
			GrabObject ();
		}

		if (Input.GetKeyUp (KeyCode.Mouse1))
		{
			ThrowObject ();
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			InteractedWithObject ();
		}

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			PowerControl ();
		}

		if (Input.GetKeyDown (KeyCode.F)) 
		{
			if (maxTorchPower > 0f) 
			{
				TorchOnOff ();
			}
		}

		if (maxTorchPower <= 0) 
		{
			torch.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ReloadTorch ();
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			isDashing = true;
			Dash ();
		}

		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			isDashing = false;
			Dash ();
		}
	}

	void Dash()
	{
		if (isDashing == true) 
		{
			movementSpeed = movementSpeed * 2f;
		}

		else if (isDashing == false) 
		{
			movementSpeed = movementSpeed / 2f;
		}
	}

	void CharacterMovements()
	{
		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime, 0f, Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime));
	}

	void Jump()
	{ 
		if (PlayerIsGrounded())
		{
			playerBody.AddRelativeForce (Vector3.up * jumpHeight, ForceMode.Impulse);
		}
	}

	bool PlayerIsGrounded()
	{
		return (Physics.Raycast (transform.position, Vector3.down, 1f));
	}

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

	void InteractedWithObject()
	{
		RaycastHit objectInteractedWith;
		Ray objectOnClick = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (objectOnClick, out objectInteractedWith)) 
		{
			if (objectInteractedWith.transform.name == "Dispenser1")
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					if (dispenser.objectWasSpawned == false) 
					{
						dispenser.SpawnObject1 ();
					}
				}
			}

			if (objectInteractedWith.transform.name == "Dispenser2")
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					if (dispenser.objectWasSpawned == false) 
					{
						dispenser.SpawnObject2 ();
					}
				}
			}
			if (objectInteractedWith.transform.name == "Dispenser3")
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					if (dispenser.objectWasSpawned == false) 
					{
						dispenser.SpawnObject3 ();
					}
				}
			}

			if (objectInteractedWith.transform.name == "OminousButton") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					gravCon.gravityIsSwapped = !gravCon.gravityIsSwapped;
					RotateCharacter ();
				}
			}

			if (objectInteractedWith.transform.name == "Battery") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					batteriesLeft++;
					battPick.hasBeenUsed = true;
				}
			}
		}
	}

	void PowerControl()
	{
		pushForce += 5f;

		if (pushForce >= 30f) 
		{
			pushForce = 0;
		}
	}

	void ConsumeTorchPower()
	{
		if (torch.activeInHierarchy) 
		{
			maxTorchPower -= 5f * Time.deltaTime;
		}
	}

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

	void RotateCharacter()
	{
		if (playerHasRotated == false)
		{
			playerHasRotated = true;
			transform.RotateAround (transform.position, Vector3.forward, 180f);
			playerHasRotated = false;
		}
	}

	void ReloadTorch()
	{
		if (batteriesLeft > 0) 
		{
			batteriesLeft -= 1f;
			maxTorchPower = 100f;
		}
	}
}

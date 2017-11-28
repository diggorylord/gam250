using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed;
	public float jumpHeight;
	public float lookSpeed;
	public float objectPickupProximity;
	public float pushForce;
	public GameObject collectedObject;
	public GameObject cameraGameObject;
	public GameObject torch;

	private Rigidbody playerBody;
	private ObjectDispenser dispenser;
	private LevelChangerButton buttonScript;

	void Start ()
	{
		dispenser = GameObject.Find ("Dispenser1").GetComponent<ObjectDispenser> ();
		buttonScript = GameObject.Find ("LevelChangeButton").GetComponent<LevelChangerButton> ();
		playerBody = GetComponent<Rigidbody> ();
		cameraGameObject = Camera.main.gameObject;
	}

	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * lookSpeed, 0f);

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
			TorchOnOff ();
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
			playerBody.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
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

			if (objectInteractedWith.transform.name == "LevelChangeButton") 
			{
				if (Vector3.Distance (transform.position, objectInteractedWith.transform.position) <= objectPickupProximity) 
				{
					buttonScript.LoadNextScene ();
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
}

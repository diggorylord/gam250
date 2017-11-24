using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed;
	public float jumpHeight;
	public float lookSpeed;
	public float pushForce;
	public float objectPickupProximity;
	public GameObject collectedObject;
	public GameObject cameraGameObject;

	private Vector3 moveDirection = Vector3.zero;
	private ObjectDispenser dispenser;

	void Start ()
	{
		dispenser = GameObject.Find ("Dispenser1").GetComponent<ObjectDispenser> ();
		cameraGameObject = Camera.main.gameObject;
	}

	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * lookSpeed, 0f);

		CharacterController controls = GetComponent<CharacterController> ();

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= movementSpeed;

		controls.Move (moveDirection * Time.deltaTime);

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

		if (Input.GetKey (KeyCode.LeftShift))
		{
			MoveDown ();
		}

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			PowerControl ();
		}
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

	void MoveDown()
	{
		if (transform.position.y > 0f)
		{
			gameObject.transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
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
}

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

	// Use this for initialization
	void Start ()
	{
		cameraGameObject = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * lookSpeed, 0f);

		CharacterController controls = GetComponent<CharacterController> ();

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= movementSpeed;

		controls.Move (moveDirection * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.Mouse1)) 
		{
			GrabObject ();
		}

		if (Input.GetKeyDown (KeyCode.Mouse0))
		{
			DropObject ();
		}

		if (Input.GetKey (KeyCode.Space)) 
		{
			MoveUp ();
		}

		if (Input.GetKey (KeyCode.LeftShift))
		{
			MoveDown ();
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

	void DropObject()
	{
		if (collectedObject != null) 
		{
			collectedObject.transform.parent = null;
			collectedObject.GetComponent<Rigidbody> ().isKinematic = false;
			collectedObject.GetComponent<Rigidbody> ().AddExplosionForce (pushForce, cameraGameObject.transform.position, 50f);
			collectedObject = null;
		}
	}

	void MoveUp()
	{
		gameObject.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
	}

	void MoveDown()
	{
		gameObject.transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
	}
}

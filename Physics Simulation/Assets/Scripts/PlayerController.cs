using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	public float lookSpeed;
	public float pushForce;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		
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
		moveDirection *= speed;

		controls.Move (moveDirection * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.Mouse1)) 
		{
			RaycastHit forceHit;
			Ray forceOnClick = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (forceOnClick, out forceHit)) 
			{
				forceHit.rigidbody.AddExplosionForce (pushForce, forceHit.point, 10f);
			}
		}
	}
}

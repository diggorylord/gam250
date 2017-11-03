using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForcesToObject : MonoBehaviour
{
	public float forceToUse;

	private Rigidbody cubeBody;


	void Start ()
	{
		cubeBody = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		
	}

	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Return)) 
		{
			cubeBody.AddForce (Vector3.up, ForceMode.Impulse);
		}
	}
}

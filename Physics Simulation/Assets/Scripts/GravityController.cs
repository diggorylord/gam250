using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	public float gravity;

	private float physicsRadius = 100000000000000000000f;

	void Start ()
	{
		
	}

	void FixedUpdate ()
	{
		Collider[] allOtherObjects = Physics.OverlapSphere (transform.position, physicsRadius);

		foreach (Collider affected in allOtherObjects)
		{
			Rigidbody otherObjects = affected.GetComponent<Rigidbody> ();

			if (affected != null)
			{
				if (affected.tag != "Environment" && affected.tag != "Player") 
				{
					otherObjects.AddForce (Vector3.down * gravity, ForceMode.Force);
				}
			}
		}
	}
}
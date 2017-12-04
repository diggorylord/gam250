using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

	//this script is what effectively simulated gravity and in turn will simulate the physics how I need it to.
	public float gravity;
	public bool gravityIsSwapped = false;

	private float physicsRadius = 100000000000000000000f;

	void FixedUpdate ()
	{
		//This searches for all the colliders inside a massive sphere for something to apply the following effects to.
		Collider[] allOtherObjects = Physics.OverlapSphere (transform.position, physicsRadius);

		//For each collider found inside the radius, there will be a force added to all objects that don't have specific tags depending on the gravity direction.
		foreach (Collider affected in allOtherObjects)
		{
			Rigidbody otherObjects = affected.GetComponent<Rigidbody> ();

			if (affected != null)
			{
				//This swaps the gravity around so that you can walk on the ceiling.
				if (affected.tag != "Environment" && affected.tag != "Dispenser"  && affected.tag != "Battery") 
				{
					if (gravityIsSwapped == true) 
					{
						otherObjects.AddForce (Vector3.up * gravity, ForceMode.Force);
					}
					else 
					{
						otherObjects.AddForce (Vector3.down * gravity, ForceMode.Force);
					}
				}
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

	//this script is what effectively simulated gravity and in turn will simulate the physics how I need it to.
	public bool gravityIsSwapped = false;
	public bool gravityIsReduced = false;

	private float gravity;
	private float gravityReducedValue = 5f;
	private float gravityRegularValue = 20f;
	private float physicsRadius = 100000000000000000000f;

	void Update()
	{
		if (gravityIsReduced == true) 
		{
			gravity = gravityReducedValue;
		} 
		else 
		{
			gravity = gravityRegularValue;
		}
	}

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
				/*I used the tags so that anything that has these tags wont be affected by 
				 * gravity. If I was to have everything affected then the force would be too 
				 * much and objects would start moving once they hit the floor as there is too 
				 * much gravity */
				if (affected.tag != "Environment" && affected.tag != "Dispenser" && affected.tag != "Page") 
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserPosition : MonoBehaviour
{
	public GameObject position1;
	public GameObject position2;
	public GameObject dispenser;

	private bool isInMainRoom = true;

	//This script is for moving the dispenser when you leave the main room. For some reason having two dispensers in my game breaks them so I only have one to move around
	void Update()
	{
		//If it is not in the main rooms then it will be outside in the terrain for use.
		if (isInMainRoom == false) 
		{
			dispenser.transform.position = position2.transform.position;
		}

		//if it is in the main rooms it will be inside the rooms and not in the terrain.
		if (isInMainRoom == true) 
		{
			dispenser.transform.position = position1.transform.position;
		}
	}

	//CHecking of the player collided with it so it can go ahead and move the dispenser.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			DispenserPositionToggle ();
		} 
	}

	//This toggles the bool to be either true or false.
	void DispenserPositionToggle()
	{
		isInMainRoom = !isInMainRoom;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

	// These are the public variables for the buttons so that they can interact with objects that have the same tag as one I specify to open a door.
	public string objectTag;
	public GameObject door;

	// These are timers and bools to do a reload function so that the door can shut after a time.
	private float doorOpenTimer = 5f;
	private bool doorIsOpen = false;

	void Update()
	{
		// This section checks whether the door has been opened using the bool and then, after a timer, the door will close again. This is to give the player a sense of urgency.
		if (doorIsOpen == true)
		{
			doorOpenTimer -= Time.deltaTime;
			if (doorOpenTimer <= 0) 
			{
				doorIsOpen = false;
				//This calls the close door method so that the door actually closes.
				CloseDoor ();
				doorOpenTimer = 5f;
			}
		}
	}

	// This is to check if the button has collided with the correct object with the required tag. If the object had the correct tag, the door opens. If not, it wont.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == objectTag) 
		{
			doorIsOpen = true;
			OpenDoor ();
		}
	}

	//This is the method that opens the door.
	void OpenDoor()
	{
		if (doorIsOpen == true) 
		{
			door.SetActive (false);
		}
	}

	//This closes the door again after a set amount of time.
	void CloseDoor()
	{
		if (doorIsOpen == false) 
		{
			door.SetActive (true);
		}
	}
}

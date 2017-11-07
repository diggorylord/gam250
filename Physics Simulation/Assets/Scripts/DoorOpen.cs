using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
	public string objectTag;
	public GameObject door;

	private float doorOpenTimer = 5f;
	private bool doorIsOpen = false;

	void Update()
	{
		if (doorIsOpen == true)
		{
			doorOpenTimer -= Time.deltaTime;
			if (doorOpenTimer <= 0) 
			{
				doorIsOpen = false;
				CloseDoor ();
				doorOpenTimer = 5f;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == objectTag) 
		{
			doorIsOpen = true;
			OpenDoor ();
		}
	}

	void OpenDoor()
	{
		if (doorIsOpen == true) 
		{
			door.SetActive (false);
		}
	}

	void CloseDoor()
	{
		if (doorIsOpen == false) 
		{
			door.SetActive (true);
		}
	}
}

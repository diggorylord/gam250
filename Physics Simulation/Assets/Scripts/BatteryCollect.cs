using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollect : MonoBehaviour
{
	//This is for grabbing the script on the player object so i can edit it from here.
	private PlayerController playCon;

	void Start()
	{
		playCon = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	//This is so that the batteires destory themselves after adding to the player's battery collection. Acts more like collectable than most batteries in games. 
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			playCon.batteriesLeft++;
			Destroy (gameObject);
		}
	}
}

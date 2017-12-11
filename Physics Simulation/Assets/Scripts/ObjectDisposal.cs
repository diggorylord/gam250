using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisposal : MonoBehaviour 
{
	private float destroyObjectTimer = 3f;

	//This destroys objects that collide with it. It's used for the rubbish bin within my scene that you can put objects into. Stops clutter.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Environment" && other.tag != "Player" && other.tag != "Dispenser") 
		{
			Destroy (other.gameObject, destroyObjectTimer);
		}
	}
}

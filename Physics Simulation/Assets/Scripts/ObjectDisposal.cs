using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisposal : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Environment" && other.tag != "Player" && other.tag != "Dispenser") 
		{
			Destroy (other.gameObject, 3f);
		}
	}
}

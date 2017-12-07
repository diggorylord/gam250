using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageCollection : MonoBehaviour
{
	//Script for collecting the pages once the player collides with them.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Destroy (gameObject);
		}
	}
}

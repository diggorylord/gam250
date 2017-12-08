using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour 
{
	public GameObject objectToSetActive;

	private bool textIsActive = false;
	private float timer = 3f;

	void Update()
	{
		if (textIsActive == true) 
		{
			timer -= Time.deltaTime;
			if (timer <= 0) 
			{
				objectToSetActive.SetActive (false);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			textIsActive = true;
			objectToSetActive.SetActive (true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour 
{
	public GameObject objectToSetActive;

	private bool textIsActive = false;
	private float timer = 3f;
	private float timerRanOut = 0f;

	//This sets the active object inactive after 3 seconds. This is to stop cluttering of UI in the game.
	void Update()
	{
		if (textIsActive == true) 
		{
			timer -= Time.deltaTime;
			if (timer <= timerRanOut) 
			{
				objectToSetActive.SetActive (false);
			}
		}
	}

	//This sets an object active when you enter its trigger. This is for setting a tutorial text aspect active so it tells you in game how to use the item you enter the trigger of.
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			textIsActive = true;
			objectToSetActive.SetActive (true);
		}
	}
}

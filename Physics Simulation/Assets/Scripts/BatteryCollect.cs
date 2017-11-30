using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollect : MonoBehaviour
{
	public bool hasBeenUsed = false;

	void Update()
	{
		if (hasBeenUsed == true) 
		{
			Destroy (gameObject);
			hasBeenUsed = false;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpoonScript : MonoBehaviour 
{
	[SerializeField]
	List<SpoonData> spoonData = new List<SpoonData>();

	[SerializeField]
	bool repeatingSpooner = false;

	int currentSpoonIndex = 0;

	[SerializeField]
	Transform[] spoonPoints;

	bool spooningDone = false;
	int currentSpoonCount = 0;

	void Update () 
	{
		if (spoonData.Count > 0 && !spooningDone)
		{
			SpoonData currentSpoonData = spoonData [currentSpoonIndex];
			if (currentSpoonData.spoonAmount > currentSpoonCount) 
			{
				currentSpoonIndex++;
				currentSpoonCount = 0;
				if (repeatingSpooner && currentSpoonIndex > spoonData.Count) 
				{
					currentSpoonIndex = 0;
				}
				else 
				{
					spooningDone = true;
				}

				Spoon (currentSpoonData);
			}
		}
	}

	void Spoon(SpoonData spoonData)
	{
		GameObject spoon = (GameObject)Instantiate (spoonData.objectToSpoon, spoonPoints [Random.Range (0, spoonPoints.Length - 1)]);
		currentSpoonCount++;
	}
}

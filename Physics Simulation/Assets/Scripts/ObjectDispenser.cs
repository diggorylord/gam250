using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
	public GameObject[] objectsToSpawn;
	public GameObject positionToSpawn;
	public bool objectWasSpawned = false;

	private float timeToSpawnObject = 3f;

	void Update()
	{
		if (objectWasSpawned == true)
		{
			timeToSpawnObject -= Time.deltaTime;

			if (timeToSpawnObject <= 0) 
			{
				Debug.Log ("Cooldown finished");
				objectWasSpawned = false;
				timeToSpawnObject = 3f;
			}
		}
	}

	public void SpawnRandomObject()
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed = Instantiate (objectsToSpawn [(Random.Range (0, objectsToSpawn.Length))], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectWasSpawned = true;
		}
	}
}

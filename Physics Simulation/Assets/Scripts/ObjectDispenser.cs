using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
	public GameObject[] objectToSpawn;
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

	public void SpawnObject1()
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed = Instantiate (objectToSpawn[0], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectWasSpawned = true;
		}
	}

	public void SpawnObject2()
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed = Instantiate (objectToSpawn[1], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectWasSpawned = true;
		}
	}

	public void SpawnObject3()
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed = Instantiate (objectToSpawn[2], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectWasSpawned = true;
		}
	}
}
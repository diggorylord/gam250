using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
	public GameObject[] objectsToSpawn;
	public GameObject positionToSpawn;

	private float timeToSpawnObject = 5f;
	private bool objectWasSpawned = false;

	void Update()
	{
		if (objectWasSpawned == true) 
		{
			timeToSpawnObject -= Time.deltaTime;
			if (timeToSpawnObject <= 0) 
			{
				SpawnRandomObject ();
				timeToSpawnObject = 5f;
				objectWasSpawned = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			objectWasSpawned = true;
		}
	}

	void SpawnRandomObject()
	{
		if (objectWasSpawned == true) 
		{
			if (timeToSpawnObject <= 0) 
			{
				GameObject objectDispensed = Instantiate (objectsToSpawn [(Random.Range (0, objectsToSpawn.Length))], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
	//These are public variables for what objects to spawn into the scene, where to spawn them and whether they were spawned or not.
	public GameObject[] objectToSpawn;
	public GameObject positionToSpawn;
	public bool objectWasSpawned = false;

	//this private variable is just for a timer for a reload function.
	private float timeToSpawnObject = 3f;

	void Update()
	{
		//This controls the cooldown of the spawner so that you cannot spam objects into the scene.
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

	/* This area is for the type of object to spawn. This made it easier for the player to spawn a certain object depending on the
	 * button that they press. each method will spawn a different item from it so that the different buttons work when the player
	 * presses them. */
	public void SpawnObject1() //Spawns object 1
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed1 = Instantiate (objectToSpawn[0], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectDispensed1.transform.Rotate (Vector3.forward, 180f);
			objectWasSpawned = true;
		}
	}

	public void SpawnObject2() //Spawns object 2
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed2 = Instantiate (objectToSpawn[1], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectDispensed2.transform.Rotate (Vector3.forward, 180f);
			objectWasSpawned = true;
		}
	}

	public void SpawnObject3() //Spawns object 3
	{ 
		if (objectWasSpawned == false)
		{
			print ("Object has been spawned. Cooldown initiated");
			GameObject objectDispensed3 = Instantiate (objectToSpawn[2], positionToSpawn.transform.position, Quaternion.identity) as GameObject;
			objectDispensed3.transform.Rotate (Vector3.forward, 180f);
			objectWasSpawned = true;
		}
	}
}
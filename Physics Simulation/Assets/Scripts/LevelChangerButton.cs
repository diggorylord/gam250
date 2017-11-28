using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerButton : MonoBehaviour 
{
	public string sceneToLoad;

	private bool hasPressedButton = false;

	public void LoadNextScene()
	{
		hasPressedButton = true;

		if (hasPressedButton == true) 
		{
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}

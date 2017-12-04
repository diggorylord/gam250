using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour 
{

	//This script is just for the main menu UI so that you can quit the game or play the game depending on the button you press.
	public void PlayGame(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

		#else

		Application.Quit();

		#endif
	}
}

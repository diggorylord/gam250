﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	public void PlayGame(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

		#else

		Application.QuitGame();

		#endif
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursorUnlock : MonoBehaviour 
{
	//This is to unlock the cursor on the main menu after finishing the level.
	void Update()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}

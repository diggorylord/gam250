using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour 
{
	//Public variables for the UI.
	public Text uiText;
	public Text uiText2;
	public Text uiText3;
	public Image fillImage;
	public GameObject tutorialSprint;
	public string sceneToLoad;
	public GameObject pauseMenu;

	//private variables for the scripts im changing from this one and a timer for one UI that tells you how to sprint.
	private PlayerController controlScript;
	private float tutorialTimer = 3f;


	void Start()
	{
		controlScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}
		
	void Update()
	{
		int pagesLeft = GameObject.FindGameObjectsWithTag ("Page").Length; //Gets an int for how many pages are in the game.
		uiText.text = "Throw Power: " + controlScript.pushForce; //UI for telling player what their throw power is.
		uiText2.text = "Batteries Left: " + controlScript.batteriesLeft; //UI for telling the player how many batteries they have.
		uiText3.text = "Pages left to collect: " + pagesLeft; // UI for telling the player how many pages there are left to collect.
		fillImage.fillAmount = controlScript.maxTorchPower / 100f; // UI bar for how much power the player's torch has left.

		//This is for the sprint tutorial aspect that will disappear after 5 seconds so it doesnt fill the screen too much.
		tutorialTimer -= Time.deltaTime;
		if (tutorialTimer <= 0)
		{
			tutorialSprint.SetActive (false);
		}

		// Once the player has collected all the pages then the next level will load.
		if (pagesLeft <= 0) 
		{
			SceneManager.LoadScene (sceneToLoad);
		}

		//For the pause menu so that you can exit to the main menu if you want to or if you cannot complete the level.
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (pauseMenu.activeInHierarchy) 
			{
				pauseMenu.SetActive (false);
				Time.timeScale = 1f;
			}

			else 
			{
				pauseMenu.SetActive (true);
				Time.timeScale = 0f;
			}
		}
	}
}

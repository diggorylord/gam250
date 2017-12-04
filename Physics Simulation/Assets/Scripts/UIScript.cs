using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour 
{
	//Public variables for the UI.
	public Text uiText;
	public Text uiText2;
	public Image fillImage;
	public GameObject tutorialSprint;

	//private variables for the scripts im changing from this one and a timer for one UI that tells you how to sprint.
	private PlayerController controlScript;
	private float tutorialTimer = 5f;


	void Start()
	{
		controlScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	//This just sets the UI to tell the player how much battery is left in their torch, how many batteries they have left and what power their throw is at.
	void Update()
	{
		uiText.text = "Throw Power: " + controlScript.pushForce;
		uiText2.text = "Batteries Left: " + controlScript.batteriesLeft;
		fillImage.fillAmount = controlScript.maxTorchPower / 100f;

		//This is for the sprint tutorial aspect that will disappear after 5 seconds so it doesnt fill the screen too much.
		tutorialTimer -= Time.deltaTime;
		if (tutorialTimer <= 0)
		{
			tutorialSprint.SetActive (false);
		}
				
	}
}

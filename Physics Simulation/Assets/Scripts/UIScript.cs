using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour 
{
	public Text uiText;
	public Text uiText2;
	public Image fillImage;

	private PlayerController controlScript;


	void Start()
	{
		controlScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update()
	{
		uiText.text = "Throw Power: " + controlScript.pushForce;
		uiText2.text = "Batteries Left: " + controlScript.batteriesLeft;
		fillImage.fillAmount = controlScript.maxTorchPower / 100f;
	}
}

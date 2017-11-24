using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour 
{
	public Text uiText;

	private PlayerController controlScript;

	void Start()
	{
		controlScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update()
	{
		uiText.text = "Throw Power: " + controlScript.pushForce;
	}
}

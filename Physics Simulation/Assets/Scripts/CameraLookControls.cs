using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookControls : MonoBehaviour
{
	//This script is for controlling the camera rotation of up and down for the look controls.
	public float rotationymin;
	public float rotationymax;
	public float lookSpeed;

	private float rotationy;
	private UIScript ui;

	//Grabs the ui script to check one of it's values.
	void Start()
	{
		ui = GameObject.Find ("InGameUI").GetComponent<UIScript> ();
	}

	//Checks if game is paused. If it is, then you cannot move around with the mouse. If not then gameplay is normal.
	void Update ()
	{
		if (ui.pauseMenu.activeInHierarchy) 
		{
			rotationy = Input.GetAxis ("Mouse Y");
		} 
		else 
		{
			rotationy += Input.GetAxis ("Mouse Y") * lookSpeed; //Rotates camera.
			rotationy = Mathf.Clamp (rotationy, rotationymin, rotationymax); //Sets a minimum and maximum value so you cannot do a 360 around your body.
			transform.localEulerAngles = new Vector3 (rotationy, transform.localEulerAngles.y, 0f);
		}
	}
}

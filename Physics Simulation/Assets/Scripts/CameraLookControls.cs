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

	//
	void Update ()
	{
		//These set the rotation so that the camera cannot rotate past a certain point. so you cannot do a full 360 around your body by moving up or down.
		rotationy += Input.GetAxis ("Mouse Y") * lookSpeed;
		rotationy = Mathf.Clamp (rotationy, rotationymin, rotationymax);
		transform.localEulerAngles = new Vector3 (rotationy, transform.localEulerAngles.y, 0f);
	}
}

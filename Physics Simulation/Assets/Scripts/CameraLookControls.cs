using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookControls : MonoBehaviour
{
	public float rotationymin;
	public float rotationymax;
	public float lookSpeed;

	private float rotationy;

	void Update ()
	{
		rotationy += Input.GetAxis ("Mouse Y") * lookSpeed;
		rotationy = Mathf.Clamp (rotationy, rotationymin, rotationymax);
		transform.localEulerAngles = new Vector3 (rotationy, transform.localEulerAngles.y, 0f);
	}
}

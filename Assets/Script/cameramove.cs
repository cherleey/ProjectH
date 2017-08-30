using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	float fDistance = 10.0f;
	float fX = 0.0f;
	float fY = 0.0f;
	float fCameraSpeed = 2.0f;

	public Transform comTarget;

	// Use this for initialization
	void Start () {
		fY = -45;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		Vector3 vPosition = comTarget.position;
		fX += Input.GetAxis ("Mouse X") * fCameraSpeed;
		fY += Input.GetAxis ("Mouse Y") * fCameraSpeed;

		vPosition -= Quaternion.Euler (-fY, fX, 0.0f) * Vector3.forward * fDistance;

		transform.position = vPosition;
		transform.LookAt (comTarget);
	}
}

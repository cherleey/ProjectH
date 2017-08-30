using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	
	enum SelectedCharater {WARRIOR, MAGE, ARCHER};

	Transform comTarget;
	float fDistance = 10.0f;
	float fX = 0.0f;
	float fY = 0.0f;
	float fCameraSpeed = 2.0f;
	SelectedCharater eSelected;

	// Use this for initialization
	void Start () {
		fY = -45;
		eSelected = SelectedCharater.WARRIOR;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterChange ();
	}

	void LateUpdate()
	{
		Vector3 vPosition = comTarget.position;
		fX += Input.GetAxis ("Mouse X") * fCameraSpeed;
		fY += Input.GetAxis ("Mouse Y") * fCameraSpeed;

		if (fY > -10)
			fY = -10.0f;
		if (fY < -60.0f)
			fY = -60.0f;

		vPosition -= Quaternion.Euler (-fY, fX, 0.0f) * Vector3.forward * fDistance;

		transform.position = vPosition;
		transform.LookAt (comTarget);
	}

	void CharacterChange()
	{
		if (Input.GetKeyDown (KeyCode.F1))
			eSelected = SelectedCharater.WARRIOR;

		if (Input.GetKeyDown (KeyCode.F2))
			eSelected = SelectedCharater.MAGE;

		if (Input.GetKeyDown (KeyCode.F3))
			eSelected = SelectedCharater.ARCHER;

		/*
		switch (eSelected) {
		case 0:
			comTarget = GameObject.Find ("2Handed Warrior").transform;
			break;

		case 1:
			comTarget = GameObject.Find ("Mage Warrior").transform;
			break;

		case 2:
			comTarget = GameObject.Find ("Archer Warrior").transform;
			break;
		}
		*/
	}
}

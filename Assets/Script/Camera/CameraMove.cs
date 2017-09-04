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
	Vector3 vVelocity = new Vector3(0.5f, 0.5f, 0.5f);
	SelectedCharater eSelected;
	bool bMoving = false;
	Vector3 vOriginCameraPos;
	Vector3 vOriginLookPos;
	float fTime = 0.0f;

	// Use this for initialization
	void Start () {
		comTarget = GameObject.Find ("2Handed Warrior").transform;
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

		if (!bMoving) {
			fX += Input.GetAxis ("Mouse X") * fCameraSpeed;
			fY += Input.GetAxis ("Mouse Y") * fCameraSpeed;
		}

		if (fY > -10)
			fY = -10.0f;
		if (fY < -60.0f)
			fY = -60.0f;

		vPosition -= Quaternion.Euler (-fY, fX, 0.0f) * Vector3.forward * fDistance;

		if (bMoving) {
			fTime += Time.deltaTime;
			transform.position = Vector3.SmoothDamp (transform.position, vPosition, ref vVelocity, 0.2f);
		} else {
			transform.position = vPosition;
			transform.LookAt (comTarget);
		}

		if (fTime >= 0.7f) {
			fTime = 0.0f;
			bMoving = false;
		}
	}

	void CharacterChange()
	{
		if (Input.GetKeyDown (KeyCode.F1)) {
			if(eSelected == SelectedCharater.MAGE)
				comTarget.GetComponent<MageControl> ().SetSelected (false);
			//else if(eSelected == SelectedCharater.ARCHER)
			//	comTarget.GetComponent<ArcherControl> ().SetSelected (false);

			eSelected = SelectedCharater.WARRIOR;
		}

		if (Input.GetKeyDown (KeyCode.F2)) {
			if(eSelected == SelectedCharater.WARRIOR)
				comTarget.GetComponent<WarriorControl> ().SetSelected (false);
			//else if(eSelected == SelectedCharater.ARCHER)
			//	comTarget.GetComponent<ArcherControl> ().SetSelected (false);

			eSelected = SelectedCharater.MAGE;
		}

		if (Input.GetKeyDown (KeyCode.F3)) {
			if(eSelected == SelectedCharater.WARRIOR)
				comTarget.GetComponent<WarriorControl> ().SetSelected (false);
			else if(eSelected == SelectedCharater.MAGE)
				comTarget.GetComponent<MageControl> ().SetSelected (false);
			
			eSelected = SelectedCharater.ARCHER;
		}
		
		switch (eSelected) {
		case SelectedCharater.WARRIOR:
			if (comTarget != GameObject.Find ("2Handed Warrior").transform) {
				comTarget = GameObject.Find ("2Handed Warrior").transform;
				bMoving = true;
				comTarget.GetComponent<WarriorControl> ().SetSelected (true);
			}
			break;

		case SelectedCharater.MAGE:
			if (comTarget != GameObject.Find ("Mage Warrior").transform) {
				comTarget = GameObject.Find ("Mage Warrior").transform;
				bMoving = true;

				comTarget.GetComponent<MageControl> ().SetSelected (true);
			}
			break;

		case SelectedCharater.ARCHER:
			if (comTarget != GameObject.Find ("Archer Warrior").transform) {
				comTarget = GameObject.Find ("Archer Warrior").transform;
				bMoving = true;

				//comTarget.GetComponent < ArcherControl> ().SetSelected (true);
			}
			break;
		}

	}
}

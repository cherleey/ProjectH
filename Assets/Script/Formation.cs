using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour {

	Transform target;
	Vector3 currentPosition;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("2Handed Warrior").transform;
		currentPosition = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition.x = target.position.x;
		currentPosition.z = target.position.z;
		currentPosition.y = 0.0f;

		transform.position = currentPosition;
		transform.rotation = target.rotation;

		ChangeTarget ();
	}

	void ChangeTarget()
	{
		if (Input.GetKeyDown (KeyCode.F1))
			target = GameObject.Find ("2Handed Warrior").transform;

		if (Input.GetKeyDown (KeyCode.F2))
			target = GameObject.Find ("Mage Warrior").transform;

		if (Input.GetKeyDown (KeyCode.F3))
			target = GameObject.Find ("Archer Warrior").transform;
	}
}

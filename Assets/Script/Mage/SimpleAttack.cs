﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour {

	public GameObject mage;

	float speed = 10.0f;
	int damage = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * -speed);
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			collision.gameObject.GetComponent<Boss> ().Hit (damage);
			Destroy (gameObject);
		}
	}
}

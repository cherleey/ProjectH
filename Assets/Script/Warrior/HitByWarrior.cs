﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByWarrior : MonoBehaviour {

	public GameObject warrior;

	int damage = 5;
	int agro = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy") &&  warrior.GetComponent<WarriorControl>().IsAttacking()) {
			collision.gameObject.GetComponent<Boss> ().Hit (damage);
			warrior.GetComponent<WarriorControl> ().SetAttacking (false);
			collision.gameObject.GetComponent<Boss> ().Agro (agro);
		}
	}
}

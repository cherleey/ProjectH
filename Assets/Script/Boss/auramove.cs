﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auramove : MonoBehaviour {
	Transform auraposition;

	// Use this for initialization
	void Start () {
		auraposition = GameObject.Find ("dragon").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = auraposition.position;

	}
}

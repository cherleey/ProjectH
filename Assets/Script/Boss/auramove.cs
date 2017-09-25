using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auramove : MonoBehaviour {
	 public Transform auraposition;

	// Use this for initialization
	void Start () {
		auraposition = GameObject.Find ("auraposition").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = auraposition.position;
		Destroy (gameObject, 4.0f);
	}
}

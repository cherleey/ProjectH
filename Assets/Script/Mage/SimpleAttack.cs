using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour {

	float speed = 10.0f;
	int damage = 5;
	int agro = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * -speed);

		Destroy (gameObject, 3.0f);
	}

	void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Debug.Log ("mageHit");
			collision.gameObject.GetComponent<Boss> ().Hit (damage);
			Destroy (gameObject);
			collision.gameObject.GetComponent<Boss> ().Agro (agro);
		}
	}
}

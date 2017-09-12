using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArrow : MonoBehaviour {

	float speed = 10.0f;
	int damage = 5;
	int agro = 2;
	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3 (90.0f, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * -speed);

		Destroy (gameObject, 3.0f);
	}

	void OnTriggerEnter(Collider collision)
	{
		
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Debug.Log ("archer Hit");
			collision.gameObject.GetComponent<Boss> ().Hit (damage);
			Destroy (gameObject);
			collision.gameObject.GetComponent<Boss> ().Agro (agro);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSight : MonoBehaviour {

	void OnTriggerEnter(Collider collision)
	{
		if (Boss.i != -1)
			return;
		if (collision.gameObject.tag == "2handed") {
			Boss.i = 0;
			Boss.target = GameObject.Find ("2Handed Warrior").transform;
			Boss.agropoint [Boss.i] += 10;
			Debug.Log ("2Handed attach");
		}if (collision.gameObject.tag == "mage"){
			Boss.target = GameObject.Find ("Mage Warrior").transform;
			Boss.i = 1;
			Boss.agropoint [Boss.i] += 10;
			Debug.Log ("mage attach");
		}if (collision.gameObject.tag == "archer"){
			Boss.target = GameObject.Find ("Archer Warrior").transform;
			Boss.i = 2;
			Boss.agropoint [Boss.i] += 10;
			Debug.Log ("archer attach");
		}

	}
}

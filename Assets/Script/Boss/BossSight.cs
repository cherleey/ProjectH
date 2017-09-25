using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSight : MonoBehaviour {

	void OnTriggerEnter(Collider collision)
	{
		if (BossAgroe.i != -1)
			return;

		if (collision.gameObject.tag == "2handed") {
			BossAgroe.i = 0;
			BossAgroe.target = GameObject.Find ("2Handed Warrior").transform;
			BossAgroe.agropoint [0] += 10;
			Debug.Log ("2Handed attach");
		}if (collision.gameObject.tag == "mage"){
			BossAgroe.target = GameObject.Find ("Mage Warrior").transform;
			BossAgroe.i = 1;
			BossAgroe.agropoint [1] += 10;
			Debug.Log ("mage attach");
		}if (collision.gameObject.tag == "archer"){
			BossAgroe.target = GameObject.Find ("Archer Warrior").transform;
			BossAgroe.i = 2;
			BossAgroe.agropoint [2] += 10;
			Debug.Log ("archer attach");
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	enum BOSSSTATE
	{
		idle,
		walk,
		run,
		attack,
		dead
	}

	BOSSSTATE boss = BOSSSTATE.idle;
	Transform target;
	CharacterController Controller;

	public GameObject BlessPrefab;
	public Animator anim;
	public Slider slider;
	public Transform blessposition;
	public Slider bosshealth;

	public float bosssight = 20.0f;
	public float bossattack =10.0f;
	public float speed = 20.0f;
	public float rot =20.0f;
	public float power = 20.0f;

	void Start () 
	{
		
		anim = GetComponent<Animator> ();
		Controller = GetComponent<CharacterController> ();
		target = GameObject.Find ("2Handed Warrior").transform;
		anim.Play ("axe|idle", -1, 0f);

	}

	void Update () 
	{
		bosshealth.value = Mathf.MoveTowards (bosshealth.value, hp, 1.0f);
		IDLE ();



		if(Input.GetKeyDown("1"))
			boss=BOSSSTATE.dead;
		if (Input.GetKeyDown ("2")) 
		{
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", true);
			GameObject obj = Instantiate (BlessPrefab);
			obj.transform.position = blessposition.position;
			obj.transform.rotation = blessposition.rotation;
			obj.GetComponent<Rigidbody> ().velocity =blessposition.forward;
			Destroy (obj, 3f);
			boss = BOSSSTATE.attack;
		}

		switch (boss) 
		{
		case BOSSSTATE.walk:

			Vector3 dir = target.position - transform.position;
			dir.y = 0.0f;
			dir.Normalize ();
			Controller.SimpleMove (dir * speed);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir),rot * Time.deltaTime);
			break;

		case BOSSSTATE.run:
			break;

		case BOSSSTATE.attack:
			break;

		case BOSSSTATE.dead:
			anim.Play("axe|dead 2");
			break;
		}


	}
	void IDLE()
	{
		if (boss == BOSSSTATE.dead)
			return;

		float distance = (target.position - transform.position).magnitude;

		if (distance < bosssight && distance > bossattack) {			
			if (anim.GetBool ("walk") == false) {
				anim.SetBool ("attack", false);
				anim.SetBool ("walk", true);
				boss = BOSSSTATE.walk;
			}
		}
		else if (distance < bossattack) {			
			if (anim.GetBool ("attack") == false) {
				anim.SetBool ("attack", true);
				anim.SetBool ("walk", false);
				boss = BOSSSTATE.attack;
			}
		}
	}
	float hp = 100.0f;

	public void Hit(int damage)
	{
		hp -= damage;

		Debug.Log (hp);

	}
}
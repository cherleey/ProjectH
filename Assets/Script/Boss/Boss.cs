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
	Transform warriortarget;
	Transform magetarget;
	CharacterController Controller;

	public GameObject BlessPrefab;
	public GameObject auraPrefab;
	public GameObject meteorPrefab;
	public Animator anim;
	public Transform blessposition;
	public Slider bosshealth;
	GameObject aura;

	public float bosssight = 20.0f;
	public float bossattack =10.0f;
	public float speed = 20.0f;
	public float rot =20.0f;
	public float power = 20.0f;

	int hp = 100;

	void OnTriggerEnter(Collider collision)
	{
	}

	void Start () 
	{
		
		anim = GetComponent<Animator> ();
		Controller = GetComponent<CharacterController> ();
		warriortarget = GameObject.Find ("2Handed Warrior").transform;
		magetarget = GameObject.Find ("Mage Warrior").transform;
		anim.Play ("axe|idle", -1, 0f);

		aura = Instantiate (auraPrefab);
		aura.SetActive (false);



	}

	void Update ()
	{
		
		IDLE ();
		bosshealth.value = Mathf.MoveTowards (bosshealth.value, hp, 1.0f);


		
		if (Input.GetKeyDown ("1"))
			boss = BOSSSTATE.dead;
		if (Input.GetKeyDown ("2")) {
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", true);
			GameObject obj = Instantiate (BlessPrefab);
			obj.transform.position = blessposition.position;
			obj.transform.rotation = blessposition.rotation;
			obj.GetComponent<Rigidbody> ().velocity = blessposition.forward;
			Destroy (obj, 3f);
			boss = BOSSSTATE.attack;
		}
		if (Input.GetKeyDown ("3")) {
			if (aura.activeSelf)
				aura.SetActive (false);
			else
				aura.SetActive (true);

		}
		if(Input.GetKeyDown("4"))
			{
				GameObject meteor = Instantiate(meteorPrefab);
				meteor.transform.position = transform.position;
				Destroy(meteor, 7.0f);
			}

	
		switch (boss) 
		{
		case BOSSSTATE.walk:

			Vector3 dir = warriortarget.position - transform.position;
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
		if (boss == BOSSSTATE.dead)
			return;

	}
	void IDLE()
	{
		if (boss == BOSSSTATE.dead)
			return;
		float distance=0;
		float warriordistance =(warriortarget.position - transform.position).magnitude;
		float magedistance = (magetarget.position - transform.position).magnitude;
		if (warriordistance < magedistance)
			distance = warriordistance;
		else
			distance = magedistance;
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

	public void Hit(int damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			hp = 0;
			boss = BOSSSTATE.dead;
		}
		Debug.Log (hp);

	}

}

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

	public GameObject BlessPrefab;
	public GameObject auraPrefab;
	public GameObject meteorPrefab;
	public Animator anim;
	public Transform blessposition;
	public Transform auraposition;
	public Slider bosshealth;


	public float bosssight = 20.0f;
	public float bossattack =10.0f;
	public float speed = 20.0f;
	public float rot =20.0f;
	public float power = 20.0f;
	public float distance = 0;
	public int hp = 1000;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		boss = BOSSSTATE.idle;

	}

	public void Hit(int damage)
	{
		hp -= damage;
		if (hp <= 0){
			hp = 0;
			boss = BOSSSTATE.dead;
		}
		//Debug.Log (hp);
		if (hp <= 50){
			GameObject aura = Instantiate (auraPrefab);
			aura.transform.position = auraposition.position;
		}
	}

	void Update ()
	{
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
			GameObject aura = Instantiate (auraPrefab);
			aura.transform.position = auraposition.position;

		}
		if(Input.GetKeyDown("4"))
		{
			GameObject meteor = Instantiate(meteorPrefab);
			meteor.transform.position = auraposition.position;
		}
		Debug.Log (boss);

		switch (boss) 
		{
		case BOSSSTATE.idle:
			if (!anim.GetBool ("idle")) {
				anim.SetBool ("idle", true);
			}
			Distance ();
			break;
		case BOSSSTATE.walk:

			if (anim.GetBool ("walk") == false) {
				anim.SetBool ("attack", false);
				anim.SetBool ("idle", false);
				anim.SetBool ("walk", true);
			}

			Distance ();

			Vector3 dir = BossAgroe.target.position - transform.position;
			dir.y = 0.0f;
			dir.Normalize ();
			//Debug.Log (distance);
			float x = transform.position.x - BossAgroe.target.position.x;
			float z = transform.position.z - BossAgroe.target.position.z;
			transform.Translate (new Vector3 (-x, 0.0f, -z) * speed * Time.deltaTime,Space.World);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir), rot * Time.deltaTime);

			if (distance < bossattack) {			
				boss = BOSSSTATE.attack;
			}
			break;

		case BOSSSTATE.run:
			break;

		case BOSSSTATE.attack:

			Distance ();

			if (anim.GetBool ("attack") == false) {
				anim.SetBool ("attack", true);
				anim.SetBool ("idle", false);
				anim.SetBool ("walk", false);
			}
			if (distance > bossattack) {
				boss = BOSSSTATE.walk;
			}

			break;

		case BOSSSTATE.dead:
			anim.Play("axe|dead 2");
			break;
		default:
			Debug.Log ("on the base case");
			break;

		}
		if (boss == BOSSSTATE.dead)
			return;
	}
	void Distance()
	{
		if (BossAgroe.maxIndex==0 && BossAgroe.agropoint[0]!=0) 
		{
			distance = BossAgroe.warriordistance;
			BossAgroe.target = GameObject.Find ("2Handed Warrior").transform;
			Debug.Log ("Highest agropoint = 2Handed");
			boss = BOSSSTATE.walk;
		} else if (BossAgroe.maxIndex==1 && BossAgroe.agropoint[1]!=0)
		{
			distance = BossAgroe.magedistance;
			BossAgroe.target = GameObject.Find ("Mage Warrior").transform;
			Debug.Log ("Highest agropoint = Mage");
			boss = BOSSSTATE.walk;
		} else if (BossAgroe.maxIndex==2 && BossAgroe.agropoint[2]!=0) 
		{
			distance = BossAgroe.archerdistance;
			BossAgroe.target = GameObject.Find ("Archer Warrior").transform;
			Debug.Log ("Highest agropoint = Archer");
			boss = BOSSSTATE.walk;
		}
	}

}

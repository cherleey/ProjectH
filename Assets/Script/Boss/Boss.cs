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
	public Transform warriortarget;
	public Transform magetarget;
	public Transform archertarget;
	public static Transform target;

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
	public static int i = -1;//첫 어그로 초기값
	public int hp = 1000;

	public static int[] agropoint;
	public int playersize =3;

	public float maxApprochDistance = 0.5f;

	void Start () 
	{
		
		anim = GetComponent<Animator> ();
		warriortarget = GameObject.Find ("2Handed Warrior").transform;
		magetarget = GameObject.Find ("Mage Warrior").transform;
		archertarget = GameObject.Find ("Archer Warrior").transform;
		anim.Play ("axe|idle", -1, 0f);

		agropoint = new int[playersize];

	}

	private int GetAggroed(int[] aggroPoint)
	{
		int maxIndex = 0;
		for (int b = 0; b < playersize-1; b++) {
			if (aggroPoint [maxIndex] < aggroPoint [b+1])
				maxIndex = b+1;
			
		}
		return maxIndex;
	}

	public void Agro(int agro)
	{
		if (agro == 0){
			agropoint[agro]+=1;
		}
		if (agro == 1){
			agropoint[agro]+=2;
		}
		if (agro == 2){
			agropoint[agro]+=3;
		}
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

		float distance = 0;
		float warriordistance =(warriortarget.position - transform.position).magnitude;
		float magedistance = (magetarget.position - transform.position).magnitude;
		float archerdistance = (archertarget.position - transform.position).magnitude;

		int maxIndex = GetAggroed (agropoint);
		//Debug.Log (maxIndex);
		if (maxIndex==0) 
		{
			distance = warriordistance;
			target = GameObject.Find ("2Handed Warrior").transform;
			Debug.Log ("Highest agropoint = 2Handed");
		} else if (maxIndex==1)
		{
			distance = magedistance;
			target = GameObject.Find ("Mage Warrior").transform;
			Debug.Log ("Highest agropoint = Mage");
		} else if (maxIndex==2) 
		{
			distance = archerdistance;
			target = GameObject.Find ("Archer Warrior").transform;
			Debug.Log ("Highest agropoint = Archer");
		}
		



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

	
		switch (boss) 
		{
		case BOSSSTATE.walk:
			
			Vector3 dir = target.position - transform.position;
			dir.y = 0.0f;
			dir.Normalize ();
			//Debug.Log (distance);
			float x = transform.position.x - target.position.x;
			float z = transform.position.z - target.position.z;
			transform.Translate (new Vector3 (-x, 0.0f, -z) * speed * Time.deltaTime,Space.World);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir), rot * Time.deltaTime);
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

	}

}

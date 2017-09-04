using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBoss : MonoBehaviour {

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

	public int bosssight = 20;
	public int bossattack =10;
	public int speed = 20;
	public int rot =20;
	public int power = 20;

	void Start () 
	{

		anim = GetComponent<Animator> ();
		Controller = GetComponent<CharacterController> ();
		target = GameObject.Find ("2Handed Warrior").transform;
		anim.Play ("axe|idle", -1, 0f);

	}

	void Update () 
	{
		if (boss == BOSSSTATE.dead)
			return;


		IDLE ();
		if(Input.GetKeyDown("1"))
			boss=BOSSSTATE.dead;
		if (Input.GetKeyDown ("2")) 
		{
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", true);
			GameObject obj = Instantiate (BlessPrefab);
			obj.transform.position = blessposition.position;
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

	void IDLE ()
	{
		float distance = (target.position - transform.position).magnitude;
		if (distance < bossattack) 
		{
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", true);
			boss = BOSSSTATE.attack;
		}
		else if (distance < bosssight) 
		{
			anim.SetBool ("attack", false);
			anim.SetBool ("walk", true);
			boss = BOSSSTATE.walk;
		}

	}







	//홍철의 수정

	int hp = 100;

	public void Hit(int damage)
	{
		hp -= damage;
		Debug.Log (hp);
	}
}

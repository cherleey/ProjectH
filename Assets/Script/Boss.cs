using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public Animator anim;

	public int bosssight = 20;
	public int bossattack =10;
	public int speed = 20;
	public int rot =20;
	void Start () 
	{
		
		anim = GetComponent<Animator> ();
		Controller = GetComponent<CharacterController> ();
		target = GameObject.Find ("2Handed Warrior").transform;
		anim.Play ("axe|idle", -1, 0f);

	}
	
	void Update () 
	{
		IDLE ();
		if(Input.GetKeyDown("1"))
			boss=BOSSSTATE.dead;
		Debug.Log ("state = " + boss);
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
			boss = BOSSSTATE.attack;
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", true);

		}
		else if (distance < bosssight) 
		{
			boss = BOSSSTATE.walk;
			anim.SetBool ("attack", false);
			anim.SetBool ("walk", true);

		}

	}
}

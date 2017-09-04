using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossmove : MonoBehaviour {
	enum BOSSSTATE
	{
		IDEL ,
		WALK ,
		RUN,
		ATTACK,
		DEAD
	}

	BOSSSTATE state =BOSSSTATE.IDEL;

	float stateTime = 0.0f;
	public float idleStateMaxTime = 2.0f;
	public float speed = 3.0f;
	public float rot = 20.0f;
	public float attackrange = 10f;
	public float sightrange = 50f;

	public float attackstatemaxtime =3f;

	public int attackpoint = 15;
	public int attackpoint2 = 30;

	Transform target;
	CharacterController characterControl;

	public Animator anim;
	int idle;
	int attack;
	int walk;
	int die;
	int run;
	void Awake()
	{	
		anim = GetComponent<Animator> ();
		idle = Animator.StringToHash("axe|idle");
		attack = Animator.StringToHash("axe|attack");
		walk = Animator.StringToHash("axe|walk");
		die = Animator.StringToHash("axe|dead2");
		run = Animator.StringToHash ("axe|run");
	}

	void OnEnable ()
	{
		InitBoss ();
	}
	void Start () {
		target = GameObject.Find ("2Handed Warrior").transform;
		characterControl = GetComponent<CharacterController> ();
	}

	void Update () 
	{
		Debug.Log ("Now State : " + state);

		switch (state) {
		case BOSSSTATE.IDEL:
			IDEL ();
			break;

		case BOSSSTATE.WALK:
			WALK ();
			break;
			/*
		case BOSSSTATE.RUN:
			RUN ();			
			break;
*/
		case BOSSSTATE.ATTACK:
			ATTACK ();
			break;

		case BOSSSTATE.DEAD:
			DEAD ();			
			break;
		}

	}


	void OnTriggerEnter(Collider col)
	{
		if (state==BOSSSTATE.DEAD)
			return;
		Debug.Log ("Hit");
		/*
		if (col.gameObject.tag == "Eye") 
		{
			Debug.Log ("EYE");
			state = BOSSSTATE.DAMAGE1;
		}
		else if (col.gameObject.tag == "Boom") 
		{
			state = BOSSSTATE.DAMAGE2;
		}
		*/

	}

	void IDEL()
	{

		float distance = (target.position - transform.position).magnitude;
		if (distance < sightrange) 
		{
			stateTime = 0.0f;
			state = BOSSSTATE.WALK;
		}
	}

	void WALK()
	{
		Vector3 dir = target.position - transform.position;
		BossRotate (dir);
		dir.Normalize ();
		characterControl.SimpleMove (dir * speed);

		float distance = (target.position - transform.position).magnitude;
		if (distance > attackrange) {
			anim.SetBool ("walk", true);
		}
		else if (distance < attackrange) 
		{
			anim.SetBool ("walk",false);
			anim.SetBool ("idle", true);
			state = BOSSSTATE.ATTACK;

		}


	}
	void RUN()
	{
		anim.SetBool ("run",true);
	}
	void ATTACK()
	{
		
		stateTime += Time.deltaTime;
		Vector3 dir = target.position - transform.position;



		if (stateTime > attackstatemaxtime) {
			anim.SetBool ("idle", false);
			anim.SetBool ("attack", true);

			StartCoroutine (COStunPause(2.0f));
			BossRotate (dir);
			stateTime = 0.0f;

		} /*else {
			anim.SetBool ("attack", false);
			anim.SetBool ("idle", true);
		}
*/

		float distance = (target.position - transform.position).magnitude;
		if (distance > attackrange) {
			state = BOSSSTATE.WALK;
			anim.SetBool ("attack", false);
		}

	}

	void DEAD()
	{
		anim.SetTrigger ("dead2");
	}

	public void InitBoss()
	{

		state = BOSSSTATE.IDEL;

	}

	void BossRotate(Vector3 dir)
	{
		dir.y = 0.0f;
		dir.Normalize ();
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir), rot * Time.deltaTime);

	}
	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}
}
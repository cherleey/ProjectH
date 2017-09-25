using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : MonoBehaviour {

	public Animator animator;
	public GameObject simpleAtt;
	public GameObject Formation1;
	public GameObject Formation2;

	GameObject targetEnemy;
	Vector3 inputVec;
	Vector3 targetDirection;
	float rotationSpeed = 30;
	float distanceToTarget = 0.0f;
	bool selected = false;
	bool encounter = false;
	GameObject targetFormation;
	bool changingFormation = false;

	// Use this for initialization
	void Start () {
		targetFormation = Formation1;
	}

	// Update is called once per frame
	void Update () {
		if (!selected) {
			PlayEffect ();
			AIMove ();
			ChangeFormation ();
			return;
		}

		PlayAnim ();
		PlayEffect ();
		GetCameraRelativeMovement ();
		RotateTowardMovementDirection ();
		ChangeFormation ();
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	void PlayAnim()
	{
		float z = Input.GetAxisRaw("Horizontal");
		float x = -(Input.GetAxisRaw("Vertical"));
		inputVec = new Vector3(x, 0, z);

		//Apply inputs to animator
		animator.SetFloat("Input X", z);
		animator.SetFloat("Input Z", -(x));

		if (x != 0 || z != 0 )  //if there is some input
		{
			//set that character is moving
			animator.SetBool("Moving", true);
			animator.SetBool("Running", true);
		}
		else
		{
			//character is not moving
			animator.SetBool("Moving", false);
			animator.SetBool("Running", false);
		}

		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack1Trigger");
			//FireSimpleAttack ();
			StartCoroutine (COStunPause(.6f));
		}
	}

	void GetCameraRelativeMovement()
	{  
		Transform cameraTransform = Camera.main.transform;

		// Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right= new Vector3(forward.z, 0, -forward.x);

		//directional inputs
		float v= Input.GetAxisRaw("Vertical");
		float h= Input.GetAxisRaw("Horizontal");

		// Target direction relative to the camera
		targetDirection = h * right + v * forward;
	}

	//face character along input direction
	void RotateTowardMovementDirection()  
	{
		if (inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
		}
	}

	void AIMove()
	{
		if (!encounter || changingFormation) {
			//targetEnemy = Camera.main.GetComponent<CameraMove> ().GetTarget ();

			if (Camera.main.GetComponent<CameraMove> ().GetTarget ().name.Contains ("2Handed"))
				targetEnemy = targetFormation.transform.FindChild("Location 1").gameObject;	
			else
				targetEnemy = targetFormation.transform.FindChild("Location 2").gameObject;
		}

		Vector3 targetLocation = new Vector3 (targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
		distanceToTarget = Vector3.Distance (transform.position, targetLocation);

		Vector3 dir = targetEnemy.transform.position - transform.position;
		dir.y = 0.0f;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

		if (targetEnemy.layer == LayerMask.NameToLayer ("Enemy") && !changingFormation) {
			if (distanceToTarget <= 10.0f) {
				animator.SetBool ("Moving", false);
				animator.SetBool ("Running", false);
				animator.SetTrigger ("Attack1Trigger");
				//FireSimpleAttack ();
				StartCoroutine (COStunPause (.6f));
			} else {
				animator.SetBool ("Moving", true);
				animator.SetBool ("Running", true);
			}
		} else if (distanceToTarget >= 1.0f) {
			animator.SetBool ("Moving", true);
			animator.SetBool ("Running", true);
		} else {
			animator.SetBool ("Moving", false);
			animator.SetBool ("Running", false);
			changingFormation = false;
		}
	}

	public void SetSelected(bool _selected)
	{
		selected = _selected;
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			targetEnemy = collision.gameObject;
			encounter = true;
		}
	}

	/*
	void OnTiggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			targetEnemy = Camera.main.GetComponent<CameraMove>().GetTarget();
			encounter = false;
		}
	}
	*/

	void FireSimpleAttack()
	{
		Vector3 initPosition = transform.position;
		initPosition.y = 2.0f;

		simpleAtt.transform.position = initPosition;

		if(selected)
			simpleAtt.transform.forward = -new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
		else
			simpleAtt.transform.forward = -transform.forward;
		

		Instantiate (simpleAtt);
	}

	void PlayEffect()
	{
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Attack1")) {
			if(selected)
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z)), Time.deltaTime * rotationSpeed);
			if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.4f && animator.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.4125f)
				FireSimpleAttack ();
		}
	}

	void ChangeFormation()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			targetFormation = Formation1;
			changingFormation = true;
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			targetFormation = Formation2;
			changingFormation = true;
		}
	}
}

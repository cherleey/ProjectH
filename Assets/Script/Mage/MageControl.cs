using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : MonoBehaviour {

	public Animator animator;

	GameObject targetEnemy;
	Vector3 inputVec;
	Vector3 targetDirection;
	float rotationSpeed = 30;
	float distanceToTarget = 0.0f;
	bool selected = false;
	bool encounter = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (!selected) {
			AIMove ();

			return;
		}

		PlayAnim ();
		GetCameraRelativeMovement ();
		RotateTowardMovementDirection ();
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
		if(!encounter)
			targetEnemy = targetEnemy = Camera.main.GetComponent<CameraMove>().GetTarget();
		
		distanceToTarget = Vector3.Distance (transform.position, targetEnemy.transform.position);

		Vector3 dir = targetEnemy.transform.position - transform.position;
		dir.y = 0.0f;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

		if (targetEnemy.layer == LayerMask.NameToLayer ("Enemy")) {
			if (distanceToTarget <= 10.0f) {
				animator.SetBool ("Moving", false);
				animator.SetBool ("Running", false);
				animator.SetTrigger ("Attack1Trigger");
				StartCoroutine (COStunPause (.6f));
			} else {
				animator.SetBool ("Moving", true);
				animator.SetBool ("Running", true);
			}
		} else if (distanceToTarget >= 5.0f) {
			animator.SetBool ("Moving", true);
			animator.SetBool ("Running", true);
		} else {
			animator.SetBool ("Moving", false);
			animator.SetBool ("Running", false);
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

	void OnTiggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			targetEnemy = targetEnemy = Camera.main.GetComponent<CameraMove>().GetTarget();
			encounter = false;
		}
	}
}

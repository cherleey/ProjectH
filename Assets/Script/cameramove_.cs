using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove_ : MonoBehaviour {

	#region Consts
	private const float SMOOTH_TIME = 0.3f;
	#endregion

	#region Public Properties
	public bool LockX;
	float offSetZ;
	public bool LockY;
	public bool LockZ;
	public bool useSmoothing;
	public int player;
	Transform target;

	public GameObject hudElements;
	#endregion

	#region Private Properties
	private Transform thisTransform;
	private Vector3 velocity;
	#endregion

	bool hudActive = true;

	private void Awake()
	{
		thisTransform = transform;
		velocity = new Vector3(0.5f, 0.5f, 0.5f);
		target = GameObject.Find ("2Handed Warrior").transform;

		player = 1;
	}

	void Update()
	{
		if(hudActive)
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				hudElements.SetActive (false);
				hudActive = false;
			}

		}
		else
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				hudElements.SetActive (true);
				hudActive = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			target = GameObject.Find ("2Handed Warrior").transform;

			player = 1;
			Debug.Log ("1");
		} 
		else if (Input.GetKeyDown (KeyCode.F2)) 
		{
			Debug.Log ("2");
			target = GameObject.Find ("Mage Warrior").transform;

			player = 2;
		}
		else if (Input.GetKeyDown (KeyCode.F3)) 
		{
			Debug.Log ("3");
			target = GameObject.Find ("Archer Warrior").transform;

			player = 3;
		}
	}

	// ReSharper disable UnusedMember.Local
	private void LateUpdate()
	// ReSharper restore UnusedMember.Local
	{
		var newPos = Vector3.zero;


			offSetZ = -6;
		if (useSmoothing)
		{
			newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
			newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y+4, ref velocity.y, SMOOTH_TIME);
			newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ, ref velocity.z, SMOOTH_TIME);
		}
		else
		{
			newPos.x = target.position.x;
			newPos.y = target.position.y;
			newPos.z = target.position.z;
		}
		
		#region Locks
		if (LockX)
		{
			newPos.x = thisTransform.position.x;
		}
		
		if (LockY)
		{
			newPos.y = thisTransform.position.y;
		}
		
		if (LockZ)
		{
			newPos.z = thisTransform.position.z;
		}
		#endregion

		transform.position = Vector3.Slerp(transform.position, newPos, Time.time);


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float duration = 2.0f;
	public bool isTurnedOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDuration(float _duration)
	{
		duration = _duration;
	}
}

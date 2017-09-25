using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAgroe : MonoBehaviour {
	public static int i = -1;//첫 어그로 초기값
	public static int[] agropoint;
	public static int playersize =3;
	public static int maxIndex;

	public static float warriordistance;
	public static float magedistance;
	public static float archerdistance;

	public Transform warriortarget;
	public Transform magetarget;
	public Transform archertarget;
	public static Transform target;

	public static int GetAggroed(int[] aggroPoint)
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

	void Start () {
		agropoint = new int[playersize];
		warriortarget = GameObject.Find ("2Handed Warrior").transform;
		magetarget = GameObject.Find ("Mage Warrior").transform;
		archertarget = GameObject.Find ("Archer Warrior").transform;
	
	}
	
	void Update () {
		maxIndex = GetAggroed (agropoint);

		warriordistance =(warriortarget.position - transform.position).magnitude;
		magedistance = (magetarget.position - transform.position).magnitude;
		archerdistance = (archertarget.position - transform.position).magnitude;
	}
		
}



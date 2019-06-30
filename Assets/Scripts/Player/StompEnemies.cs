using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemies : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider enter)
	{
		if (enter.tag == "EnemyWeakPoint")
		{
			enter.gameObject.GetComponentInParent<Robot>().dead = true;
		}
	}
}

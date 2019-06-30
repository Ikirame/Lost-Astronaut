using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float timeAlive;
	public Object explosion;
	private float timeOfLife = 0;
	
	// Update is called once per frame
	void Update () {
		timeOfLife += Time.deltaTime;
		if (timeOfLife >= timeAlive)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider enter)
	{
		if (enter.tag != "Player" && timeOfLife > 0.1)
		{
			GameObject cloneExplosion = Instantiate(explosion as GameObject);
			cloneExplosion.transform.position = transform.position;
			Destroy(cloneExplosion, 2);
		}
	}
}

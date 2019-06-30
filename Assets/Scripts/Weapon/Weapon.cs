using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Weapon : MonoBehaviour {

	public Object bullet;
	public float speed;

	public float Delay;
	private float lastShot;

	public Camera cam;

    [HideInInspector] public bool CanShot;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();

		lastShot = -Delay;

	    CanShot = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!CanShot)
            return;

		if (Input.GetKey(KeyCode.Mouse0))
		{
            _audioSource.Play();
			if (Time.time - lastShot >= Delay)
			{
			    GameObject cloneBullet = Instantiate(bullet as GameObject);
				//GameObject cloneBullet = PrefabUtility.InstantiatePrefab(bullet as GameObject) as GameObject;
				cloneBullet.transform.position = transform.position;
				Rigidbody rb = cloneBullet.GetComponent<Rigidbody>();
				//rb.AddForce(transform.forward * speed);
				transform.forward = cam.transform.forward;
				rb.AddForce(cam.transform.forward * speed);
				lastShot = Time.time;
			}
		}
	}
}

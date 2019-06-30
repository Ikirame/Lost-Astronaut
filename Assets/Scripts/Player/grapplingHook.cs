using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplingHook : MonoBehaviour
{

	public Camera cam;
	public RaycastHit hit;

	public LayerMask surfaces;
	public int maxDistance;

	private bool isMoving;
	public float residualForceTime;
	private float timeSinceHook;
	private Vector3 lastVelocity;
	private Vector3 location;

	public float speed = 10;
	public Transform hook;

	public PlayerController pc;
	public LineRenderer LR;

	void Start()
	{
		pc = GetComponent<PlayerController>();
		LR.enabled = false;
		timeSinceHook = residualForceTime;
	}

	void Update()
	{

		// Envois du grappin
		if (Input.GetKey(KeyCode.E))
		{
			Grapple();
		}

		// Si le personnage vole, on l'envoie vers le point d'arrivÃ©e 
		if (isMoving)
		{
			LR.SetPosition(0, transform.position);
			MoveToSpot(1);
		}
		else if (timeSinceHook < residualForceTime)
		{
			//pc.GravityScale = 0.65f;
			timeSinceHook += Time.deltaTime;
			pc._characterController.Move(lastVelocity * ((residualForceTime - timeSinceHook) / residualForceTime) * Time.deltaTime);
			//MoveToSpot(timeSinceHook / residualForceTime);
		}

		// Annulation / dÃ©crochage du grappin
		if (Input.GetKey(KeyCode.Space) && isMoving)
		{
			isMoving = false;
			pc.CanMove = true;
			LR.enabled = false;
			pc.GravityScale = 1;
			lastVelocity = pc._characterController.velocity;
			pc.MoveSpeed /= 2;
		}


	}

	// Lors de l'envois du grappin
	public void Grapple()
	{
		// On crÃ©Ã© un raycast de "maxDistance" unitÃ©s depuis la camÃ©ra vers l'avant.
		// Si ce raycast touche quelque chose c'est que la grappin est utilisable
		if (Physics.Raycast(transform.position, cam.transform.forward, out hit, maxDistance, surfaces))
		{
			if (isMoving)
				pc.MoveSpeed /= 2;
			isMoving = true;
			location = hit.point;
			//pc.CanMove = false;
			pc.GravityScale = 0;
			LR.enabled = true;
			LR.SetPosition(0, transform.position);
			LR.SetPosition(1, location);
			timeSinceHook = 0;
			pc.MoveSpeed *= 2;
		}

	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveToSpot(float multiplier)
	{
		pc._characterController.Move(Vector3.Lerp(transform.position, location, speed * multiplier * Time.deltaTime / Vector3.Distance(transform.position, location)) - transform.position);

		// Si on est Ã  - de 1 unitÃ©(s) de la cible final on dÃ©croche le grappin automatiquement
		if (Vector3.Distance(transform.position, location) < 1f)
		{
			isMoving = false;
			pc.CanMove = true;
			LR.enabled = false;
			pc.GravityScale = 1;
			timeSinceHook = residualForceTime;
			pc.MoveSpeed /= 2;
		}
	}
}
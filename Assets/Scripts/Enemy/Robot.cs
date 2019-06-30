using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
	public int life;
	public float maxDistanceSight;
	public SphereCollider safeZone;
	private GameObject player; // Reference to the player's.
	private UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
	[HideInInspector]
	private Animator m_Animator;
	[HideInInspector]
	public bool dead = false;
	private bool anim_dead = false;
	private float delta = 0;

	// Use this for initialization
	void Awake()
		{
			// Set up the references.
			nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
			m_Animator = GetComponent<Animator>();
			player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (dead)
		{
			if (anim_dead == false)
			{
				m_Animator.SetTrigger("Death");
				anim_dead = true;
			}
			delta += Time.deltaTime;
			if (m_Animator.GetCurrentAnimatorStateInfo(0).length <= delta)
				Destroy(gameObject);
		}
		else
		{
			var maxDistanceSquared = maxDistanceSight * maxDistanceSight;
			Vector3 rayDirection = player.transform.position - transform.position;

			if (rayDirection.sqrMagnitude < maxDistanceSquared && (safeZone.transform.position - player.transform.position).magnitude > safeZone.radius)
			{
				nav.SetDestination(player.transform.position);
				m_Animator.SetBool("Run", true);
			}
			else
				m_Animator.SetBool("Run", false);
		}
	}

	void OnTriggerEnter(Collider enter)
	{
		if (enter.tag == "Bullets")
		{
			life -= 1;
			if (life <= 0)
			{
				dead = true;
			}
		}
		if (enter.tag == "Player")
		{
			m_Animator.SetTrigger("Attack");
		}
        
	}
}

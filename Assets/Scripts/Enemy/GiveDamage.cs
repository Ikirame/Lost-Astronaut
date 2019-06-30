using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour {

    public int damagePoint;

    private void OnTriggerEnter(Collider collider)
    {
       if (collider.tag == "Player" && (collider.gameObject.transform.position - transform.position).normalized.y < 0.8)
        {
            Vector3 hitDirection = collider.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            collider.gameObject.GetComponent<HealthManager>().TakeDamage(damagePoint, hitDirection);
        }           
    }
}

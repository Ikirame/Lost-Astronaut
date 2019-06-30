using UnityEngine;

public class OxygenTank : MonoBehaviour {

    public float oxygenQuantity;

    public AudioClip audioClip;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");
            FindObjectOfType<HealthManager>().RefillOxygen(this, audioClip);
        }
    }
}

using UnityEngine;

public class MedPack : MonoBehaviour {

    public GameObject pickUpEffect;

    public AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<HealthManager>().HealPlayer(this, audioClip);
        }
    }
}

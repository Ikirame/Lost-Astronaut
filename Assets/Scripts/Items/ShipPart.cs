using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour {

    public enum PartType
    {
        Tail,
        Reactor,
        Wheel
    }

    public PartType type;

    private ShipPartManager manager;
    private AudioSource _audioSource;

    private void Start()
    {
        manager = FindObjectOfType<ShipPartManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            manager.PickUpPart(this);
        }
    }
}

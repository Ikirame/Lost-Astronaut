using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {


    // Variables for the player life
    public int maxLives;
    public float max0xygen;
    public float oxygenDecreaseRate;
    public float oxygenIncreaseRate;

    [HideInInspector]
    public int lives;
    
    [HideInInspector]
    public float oxygen;

    private PlayerHeartDisplay display;
    private bool regenOxygen;

    public float liveLossTime = 2;
    private float oxygenCounter;

    // Invicibility variables
    public Renderer playerRenderer;
    public Animator playerAnimator;

    public float invincibilityLength;
    private float invisibilityCounter;

    public float flashLength;
    private float flashCounter = 0.2f;

    // Audio Managment
    public AudioClip oxygen50;
    public AudioClip oxygen25;
    public AudioClip oxygen10;
    public AudioClip oxygenRegen;
    public AudioClip HurtSound;

    private AudioSource audioSource;
    private PlayerController controller; 

    // Use this for initialization
    void Start () {

        oxygen = max0xygen;
        lives = maxLives;
        display = GetComponent<PlayerHeartDisplay>();
        display.OnChangeHeart(maxLives, lives);
        display.OnChangeOxygen((int)oxygen);
        oxygenCounter = liveLossTime;

        invisibilityCounter = 0;
        flashCounter = flashLength;

        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<PlayerController>();
	}

    // Update is called once per frame
    void Update() {

        if (invisibilityCounter > 0)
        {
            invisibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invisibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (regenOxygen) {
            if (oxygen < max0xygen)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(oxygenRegen);
                }
                oxygen += oxygenIncreaseRate * Time.deltaTime;
            }

        } else {
            if (oxygen > 0)
                oxygen -= oxygenDecreaseRate * Time.deltaTime;

            // Audio warnings 
            if (oxygen > 50 && oxygen < 51 && !audioSource.isPlaying)  
                audioSource.PlayOneShot(oxygen50);
            if (oxygen > 25 && oxygen < 26 && !audioSource.isPlaying)
                audioSource.PlayOneShot(oxygen25);
            if (oxygen > 10 && oxygen < 11 && !audioSource.isPlaying)
                audioSource.PlayOneShot(oxygen10);
        }

        if (oxygen <= 0)
        {
            oxygenCounter -= Time.deltaTime;
        }

        if (oxygenCounter <= 0)
        {
            TakeDamage(1, new Vector3(0,0,0));
            oxygenCounter = liveLossTime;
        }

        display.OnChangeOxygen((int)oxygen);      
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            regenOxygen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            regenOxygen = false;
        }
    }

    public void HealPlayer(MedPack medPack, AudioClip clip)
    {
        if (lives != maxLives)
        {
            audioSource.PlayOneShot(clip);
            lives++;
            Instantiate(medPack.pickUpEffect, transform.position, transform.rotation);
            Destroy(medPack.gameObject);
            display.OnChangeHeart(maxLives, lives);
        }
    }

    public void RefillOxygen(OxygenTank tank, AudioClip clip)
    {
        float newQuantity = oxygen + tank.oxygenQuantity;

        audioSource.PlayOneShot(clip);
        oxygen = max0xygen > newQuantity ? newQuantity : max0xygen;
        Destroy(tank.gameObject);
        display.OnChangeOxygen((int)oxygen);
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (lives <= 0)
            return;

        if (invisibilityCounter <= 0)
        {
            audioSource.PlayOneShot(HurtSound);
            lives -= damage;
            GetComponent<PlayerController>().KnockBack(direction);
            display.OnChangeHeart(maxLives, lives);
            invisibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;
            flashCounter = flashLength;
        }
        if (lives == 0)
        {
            StartCoroutine(controller.Death());
        }
    }

}

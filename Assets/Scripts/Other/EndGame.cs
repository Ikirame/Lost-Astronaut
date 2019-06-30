using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject RepairPanel;
    public GameObject EndPanel;
    public GameObject DisplayPanel;
    public GameObject Minimap;
    public GameObject GameOverPanel;

    [HideInInspector]public bool Finish;

    private bool waitForPress = false;

    private ShipPartManager shipParts;

	// Use this for initialization
	void Start () {

        RepairPanel.SetActive(false);
        EndPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        shipParts = FindObjectOfType<ShipPartManager>();
	    Finish = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (waitForPress == true && Input.GetButtonDown("Inventory"))
        {
            Finish = true;
            EndGameScene();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && shipParts.HasAllShipParts())
        {
            RepairPanel.SetActive(true);
            waitForPress = true;       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        waitForPress = false;
        RepairPanel.SetActive(false);
    }

    public void EndGameScene()
    {
        var mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.None;

        var cc = mainCamera.GetComponent<CameraController>();
        cc.CanMove = false;

        Time.timeScale = 0;
        waitForPress = false;
        EndPanel.SetActive(true);
        DisplayPanel.SetActive(false);
        Minimap.SetActive(false);
        RepairPanel.SetActive(false);
    }

    public void GameOverScene()
    {
        var mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.None;

        var cc = mainCamera.GetComponent<CameraController>();
        cc.CanMove = false;

        Time.timeScale = 0;
        DisplayPanel.SetActive(false);
        Minimap.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}

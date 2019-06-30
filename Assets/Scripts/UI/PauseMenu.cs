using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Pause Panel game object
    public GameObject PausePanel;

    //Options Panel game object
    public GameObject OptionsPanel;

    //lives Display panel
    public GameObject LivesDisplay;

    public GameObject Player;

    public GameObject SpaceShip;

    // Defines if the game is paused or not
    private bool _isPaused;

    // Defines if the inventory and options panel is displayed or not
    private bool _optionsDisplayed = false;

    private HealthManager _playerHealthManager;

    private EndGame _endGame;

    private void Start()
    {
        // Sets the pause panel to enable
       PausePanel.SetActive(false);
        _isPaused = false;

        //Sets the options panel to inactive
        OptionsPanel.SetActive(false);

        _playerHealthManager = Player.GetComponent<HealthManager>();

        _endGame = SpaceShip.GetComponent<EndGame>();
    }

    private void Update()
    {
        // Checks if the button to make pause is pressed
        if (Input.GetButtonDown("Pause") && _playerHealthManager.lives > 0 && _endGame.Finish == false)
        {
            if (_optionsDisplayed)
            {
                HideOptions();
                return;
            }
            // Do pause or disable pause
            if (!_isPaused)
                DoPause();
            else
                UnPause();
        }
    }

    private void DoPause()
    {
        // Do pause
        _isPaused = true;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        var mainCamera = Camera.main;

        var cc = mainCamera.GetComponent<CameraController>();
        cc.CanMove = false;

        PausePanel.SetActive(true);
        LivesDisplay.SetActive(false);
    }

    public void UnPause()
    {
        // Disable pause
        _isPaused = false;
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;

        var mainCamera = Camera.main;

        var cc = mainCamera.GetComponent<CameraController>();
        cc.CanMove = true;

        PausePanel.SetActive(false);
        LivesDisplay.SetActive(true);
    }

    public void ShowOptions()
    {
        // Shows options panel
        _optionsDisplayed = true;
       PausePanel.SetActive(false);
       OptionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        // Hides option panel
        _optionsDisplayed = false;
        OptionsPanel.SetActive(false);
        PausePanel.SetActive(true);
    }
}
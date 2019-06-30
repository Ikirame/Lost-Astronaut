using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Menu
{
    // Name of the menu
    public string Name;

    // Collider of the menu
    public Collider Collider;

    // Canvas to display
    [SerializeField] private Canvas[] _canvasToDisplay;

    public void Display()
    {
        // Allowing the mouse over the planets
        if (Name == "PlanetMenu")
        {
            var planetForest = GameObject.Find("ForestPlanet");
            var planetSandy = GameObject.Find("SandyPlanet");

            var scriptPlanetMouseOver = planetForest.GetComponent<PlanetMouseOver>();
            scriptPlanetMouseOver.SetCanBeOver(true);

            scriptPlanetMouseOver = planetSandy.GetComponent<PlanetMouseOver>();
            scriptPlanetMouseOver.SetCanBeOver(true);
        }

        // Displaying all the canvas
        foreach (var canvas in _canvasToDisplay)
            canvas.gameObject.SetActive(true);
    }
}

public class MenuCamera : MonoBehaviour
{
    // Speed of the movement
    public float MovementSpeed = 50.0f;

    // Acceleration of the movement
    public float Acceleration = 0.1f;

    // Deceleration of the movement
    public float Deceleration = 10.0f;

    // Menus available
    public List<Menu> Menus;

    // Action speed
    private float _actSpeed;

    // Last direction of the movement
    private Vector3 _lastDir;

    // Current direction of the movement
    private Vector3 _dirVector;

    // Menu to display;
    private Menu _menuToDisplay;

    private void Update()
    {
        // Applying deceleration
        var speed = Vector3.Distance(_menuToDisplay.Collider.transform.position, transform.position) < 15.0f
            ? Deceleration
            : MovementSpeed;

        if (_dirVector != Vector3.zero)
        {
            if (_actSpeed < 1)
                _actSpeed += Acceleration * Time.deltaTime * 40;
            else
                _actSpeed = 1.0f;

            _lastDir = _dirVector;
        }
        else
        {
            if (_actSpeed > 0)
                _actSpeed -= Acceleration * Time.deltaTime * 20;
            else
                _actSpeed = 0.0f;
        }

        // Moving the camera
        transform.Translate(_lastDir * _actSpeed * speed * Time.deltaTime);
    }

    public void SetMenuToDisplay(string menuName)
    {
        // Finds the menu by name
        _menuToDisplay = Menus.Find(_ => _.Name == menuName);

        // Set the direction vector with the menu position
        _dirVector = transform.position.z > _menuToDisplay.Collider.transform.position.z
            ? Vector3.back
            : Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Displaying the menu
        _menuToDisplay.Display();

        // Disable the script
        enabled = false;
    }
}
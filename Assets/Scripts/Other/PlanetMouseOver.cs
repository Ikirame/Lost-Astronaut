using UnityEngine;

public class PlanetMouseOver : MonoBehaviour
{
    public Canvas PlanetCanvas;

    public GameObject LevelLoader;

    public GameObject LoadingPanel;

    [SerializeField] private float _scaleRate = 1.2f;

    private bool _canBeOver;

    private void OnMouseEnter()
    {
        if (!_canBeOver)
            return;

        // Updating local scale of the object
        var localScale = gameObject.transform.localScale;
        localScale *= _scaleRate;
        gameObject.transform.localScale = localScale;

        // Activates "PlanetCanvas"
        PlanetCanvas.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!_canBeOver)
            return;

        // Updating local scale of the object
        var localScale = gameObject.transform.localScale;
        localScale /= _scaleRate;
        gameObject.transform.localScale = localScale;

        // Deactivates "PlanetCanvas"
        PlanetCanvas.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        var levelLoader = LevelLoader.GetComponent<LevelLoader>();
        if (!levelLoader)
            return;

        levelLoader.LoadingScreen = LoadingPanel;
        levelLoader.LoadLevel(name + "Level");
    }

    public void SetCanBeOver(bool over)
    {
        _canBeOver = over;
    }
}
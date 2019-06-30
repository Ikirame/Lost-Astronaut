using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector]public GameObject LoadingScreen;

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAsynchronously(levelName));
    }

    private IEnumerator LoadLevelAsynchronously(string levelName)
    {
        var operation = SceneManager.LoadSceneAsync(levelName);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
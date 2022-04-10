using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Slider ProgressSlider;

    private void Start()
    {
        ProgressSlider.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        Debug.Log("start game");
        ProgressSlider.gameObject.SetActive(true);
        StartCoroutine(LoadScene("Lobby"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone)
        {
            ProgressSlider.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}

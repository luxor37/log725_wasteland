using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Slider progressSlider;

    private void Start()
    {
        progressSlider.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame(int index)
    {
        Debug.Log("start game");
        progressSlider.gameObject.SetActive(true);
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while(!operation.isDone)
        {
            progressSlider.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}

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
        Debug.Log("quit game");
        Application.Quit();
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

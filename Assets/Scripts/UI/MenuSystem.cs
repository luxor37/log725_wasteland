using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public GameObject FirstSelected;

    public Slider ProgressSlider;

    private EventSystem eventSystem;

    private int counter = 0;

    private void Start()
    {
        ProgressSlider.gameObject.SetActive(false);

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        if (eventSystem != null && FirstSelected != null)
            eventSystem.SetSelectedGameObject(FirstSelected);
        else
            Debug.Log("Could not set event system");
    }

    void Update()
    {
        counter++;

        if (counter == 30)
        {
            eventSystem.SetSelectedGameObject(FirstSelected);
        }
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


using UnityEngine;
using UnityEngine.EventSystems;
using static SceneTransitionManager;

public class PauseMenu : MonoBehaviour
{
    public GameObject FirstSelected;

    [HideInInspector]
    public static bool IsGamePaused;

    private CanvasGroup menu;

    private EventSystem eventSystem;

    void Start()
    {
        menu = gameObject.GetComponentInChildren<CanvasGroup>();
        menu.alpha = 0;
        menu.interactable = false;
        menu.blocksRaycasts = false;

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        if (InputController.IsPausing)
        {
            Pause(!IsGamePaused);
            menu.alpha = IsGamePaused ? 1 : 0;
            menu.interactable = IsGamePaused;
            menu.blocksRaycasts = IsGamePaused;
        }
    }

    void Pause(bool isPaused)
    {
        if(eventSystem != null && FirstSelected != null)
            eventSystem.SetSelectedGameObject(FirstSelected);
        else
            Debug.Log("Could not set event system");

        IsGamePaused = isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }

    public void Quit()
    {
        PersistenceManager.Reset();

        Pause(false);
        Cursor.lockState = CursorLockMode.Confined;
        SceneTransitionManagerSingleton.LoadScene("MainMenu");
    }

    public void ReturnToLobby()
    {
        Pause(false);
        SceneTransitionManagerSingleton.LoadScene("Lobby");
    }
}

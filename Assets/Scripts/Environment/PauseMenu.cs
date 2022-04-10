
using UnityEngine;
using static SceneTransitionManager;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public static bool IsGamePaused;

    private CanvasGroup menu;

    void Start()
    {
        menu = gameObject.GetComponentInChildren<CanvasGroup>();
        menu.alpha = 0;
        menu.interactable = false;
        menu.blocksRaycasts = false;
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


using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public static bool isGamePaused = false;

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
            Pause(!isGamePaused);
            menu.alpha = isGamePaused ? 1 : 0;
            menu.interactable = isGamePaused;
            menu.blocksRaycasts = isGamePaused;
        }
    }

    void Pause(bool isPaused)
    {
        isGamePaused = isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }

    public void Quit()
    {


        Pause(false);
        Cursor.lockState = CursorLockMode.Confined;
        SceneTransitionManager.sceneTransitionManager.LoadScene("MainMenu");
    }

    public void ReturnToLobby()
    {
        Pause(false);
        SceneTransitionManager.sceneTransitionManager.LoadScene("Lobby");
    }
}

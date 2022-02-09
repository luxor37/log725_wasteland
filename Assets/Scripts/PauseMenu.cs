
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

        if (isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Pause(bool isPaused)
    {
        isGamePaused = isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ReturnToLobby()
    {
        Pause(false);
        SceneManager.LoadScene("Lobby");
    }
}

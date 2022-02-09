
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
            isGamePaused = !isGamePaused;
            menu.alpha = isGamePaused? 1: 0;
            menu.interactable = isGamePaused;
            menu.blocksRaycasts = isGamePaused;

            Time.timeScale = isGamePaused ? 0 : 1;
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

    public void Quit()
    {
        Debug.Log("HELLLO");
#if UNITY_EDITOR
        Debug.Log("Quitting Editor");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("Quitting");
        Application.Quit();
#endif
    }

    public void ReturnToLobby()
    {
        Debug.Log("Returning to Lobby Scene");
        SceneManager.LoadScene("Lobby");
    }
}

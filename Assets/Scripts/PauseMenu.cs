
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public static bool isGamePaused = false;

    private GameObject menu;

    void Start(){
        menu = gameObject.GetComponentInChildren<Canvas>().gameObject;
        menu.SetActive(false);
    }

    void Update(){
        if (InputController.IsPausing)
        {
            isGamePaused = !isGamePaused;
            menu.SetActive(isGamePaused);

            Time.timeScale = isGamePaused ? 0 : 1;
        }

        if(isGamePaused){
            Cursor.lockState = CursorLockMode.Confined;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Quit(){
        Debug.Log("HELLLO");
        #if UNITY_EDITOR
        Debug.Log("Quitting Editor");
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Debug.Log("Quitting");
        Application.Quit();
        #endif
    }

    public void ReturnToLobby(){
        Debug.Log("Returning to Lobby Scene");
        SceneManager.LoadScene("Lobby");
    }
}

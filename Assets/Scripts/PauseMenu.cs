
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Quit(){
        
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

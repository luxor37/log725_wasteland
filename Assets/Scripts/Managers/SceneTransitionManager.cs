using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator SceneTransitionAnim;
    public Animator GameOverAnim;

    public static SceneTransitionManager SceneTransitionManagerSingleton;

    void Start(){
        SceneTransitionManagerSingleton = gameObject.GetComponent<SceneTransitionManager>();
    }

    private string scene;

    public void LoadScene(string sceneName){
        scene = sceneName;
        StartCoroutine(LoadSceneRoutine());
    }

    public void GameOver(){
        scene = "Lobby";
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator LoadSceneRoutine(){
        InputController.DisableControls = true;
        SceneTransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

    private IEnumerator GameOverRoutine(){
        InputController.DisableControls = true;
        GameOverAnim.SetTrigger("end");
        SceneTransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene);
    }
}

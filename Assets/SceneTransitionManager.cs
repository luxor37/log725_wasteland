using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator sceneTransitionAnim;
    public Animator gameOverAnim;

    public static SceneTransitionManager sceneTransitionManager;

    void Start(){
        sceneTransitionManager = gameObject.GetComponent<SceneTransitionManager>();
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

    IEnumerator LoadSceneRoutine(){
        sceneTransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator GameOverRoutine(){
        gameOverAnim.SetTrigger("end");
        sceneTransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
}

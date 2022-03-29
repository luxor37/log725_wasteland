using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int LevelCompletedCount = 0;
    private Collider _bounds;
    private bool _areTouching = false;

    public TextMesh instructions;



    public string sceneName;

    void Start()
    {
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Down)
            SceneTransitionManager.sceneTransitionManager.LoadScene(sceneName);
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up)
            SceneTransitionManager.sceneTransitionManager.LoadScene(SceneManager.GetActiveScene().name.Equals("SceneAleatoire") ? "LevelBoss" : "SceneAleatoire");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            _areTouching = true;
            if (instructions != null)
            {
                instructions.characterSize = 1;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            _areTouching = false;
            if (instructions != null)
            {
                instructions.characterSize = 0;
            }
        }
    }
}

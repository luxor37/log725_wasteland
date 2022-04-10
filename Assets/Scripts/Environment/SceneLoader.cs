using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneTransitionManager;

public class SceneLoader : MonoBehaviour
{
    private bool _areTouching;

    public TextMesh Instructions;
    
    public string SceneName;

    void Start()
    {
        if (Instructions != null)
        {
            Instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Down)
            SceneTransitionManagerSingleton.LoadScene(SceneName);
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up)
            SceneTransitionManagerSingleton.LoadScene(SceneManager.GetActiveScene().name.Equals("SceneAleatoire") ? "LevelBoss" : "SceneAleatoire");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = true;
        if (Instructions != null)
        {
            Instructions.characterSize = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = false;
        if (Instructions != null)
        {
            Instructions.characterSize = 0;
        }
    }
}

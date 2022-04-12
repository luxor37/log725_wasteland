using UnityEngine;
using static SceneTransitionManager;

public class SceneLoader : MonoBehaviour
{
    private bool _areTouching;

    private TextMesh instructions;

    public string SceneName;

    void Start()
    {
        instructions = GameObject.Find("interactPrompt").GetComponent<TextMesh>();
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.IsInteracting)
            SceneTransitionManagerSingleton.LoadScene(SceneName);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = true;
        if (instructions != null)
        {
            instructions.characterSize = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = false;
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Collider _bounds;
    private bool _areTouching = false;

    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Down)
            SceneManager.LoadScene(sceneName);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            _areTouching = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            _areTouching = false;
    }
}

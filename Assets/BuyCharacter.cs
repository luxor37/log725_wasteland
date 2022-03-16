using UnityEngine;

public class BuyCharacter : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up){
            
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
        if (other.gameObject.tag == "Player")
        {
            _areTouching = false;
            if (instructions != null)
            {
                instructions.characterSize = 0;
            }
        }
    }
}

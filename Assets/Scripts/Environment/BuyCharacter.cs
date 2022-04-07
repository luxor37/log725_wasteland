using Player;
using UnityEngine;

public class BuyCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider _bounds;
    private bool _areTouching = false;

    public TextMesh instructions;

    public int cost = 5;

    void Start()
    {
        if(PersistenceManager.is2ndCharacterUnlocked){
            Destroy(gameObject);
        }
        
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up){
            if(PersistenceManager.coins >= 5){
                PersistenceManager.coins -= 5;
                PersistenceManager.is2ndCharacterUnlocked = true;
                Destroy(gameObject);
            }
        }

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

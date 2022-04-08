using UnityEngine;

public class BuyCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _areTouching;

    public TextMesh Instructions;

    public int Cost = 5;

    void Start()
    {
        if(PersistenceManager.Is2NdCharacterUnlocked){
            Destroy(gameObject);
        }
        
        if (Instructions != null)
        {
            Instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up){
            if(PersistenceManager.coins >= Cost)
            {
                PersistenceManager.coins -= Cost;
                PersistenceManager.Is2NdCharacterUnlocked = true;
                Destroy(gameObject);
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            _areTouching = true;
            if (Instructions != null)
            {
                Instructions.characterSize = 1;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            _areTouching = false;
            if (Instructions != null)
            {
                Instructions.characterSize = 0;
            }
        }
    }
}

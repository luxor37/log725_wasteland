using UnityEngine;

public class BuyCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _areTouching;
    
    private TextMesh _instructions;

    public int Cost = 5;

    void Start()
    {
        _instructions = GameObject.Find("interactPrompt").GetComponent<TextMesh>();
        if (PersistenceManager.Is2NdCharacterUnlocked){
            _instructions.characterSize = 0;
            Destroy(gameObject);
        }
        
        if (_instructions != null)
        {
            _instructions.characterSize = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_areTouching || !InputController.IsInteracting) return;
        if (PersistenceManager.Coins < Cost) return;

        PersistenceManager.Coins -= Cost;
        PersistenceManager.Is2NdCharacterUnlocked = true;
        _instructions.characterSize = 0;
        Destroy(gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = true;
        if (_instructions != null && PersistenceManager.Coins >= Cost)
        {
            _instructions.characterSize = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = false;
        if (_instructions != null)
        {
            _instructions.characterSize = 0;
        }
    }
}

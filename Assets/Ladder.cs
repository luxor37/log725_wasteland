using Player;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Collider _bounds;
    private bool _areTouching = false;
    private PlayerController player;

    public TextMesh instructions;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }
    
    void Update()
    {
        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up){
            player.isClimbing = true;
            player.ladderAngle = 180;
        }

        if(InputController.HorizontalDirection != HorizontalDirection.Iddle || InputController.IsJumping){
            player.isClimbing = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player"){
            _areTouching = true;
            if (instructions != null)
            {
                instructions.characterSize = 1;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player"){
            _areTouching = false;
            player.isClimbing = false;
            if (instructions != null)
            {
                instructions.characterSize = 0;
            }
        }
    }
}

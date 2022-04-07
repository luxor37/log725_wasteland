using Player;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Collider _bounds;
    public bool _areTouching = false;
    private PlayerController player;
    public bool hideInstructions = false;
    public TextMesh instructions;

    void Start()
    {
        if (instructions != null)
        {
            instructions.characterSize = 0;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up)
            {
                player.IsClimbing = true;
                player.LadderAngle = 180;
            }

            if (InputController.HorizontalDirection != HorizontalDirection.Iddle || InputController.IsJumping)
            {
                player.IsClimbing = false;
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            _areTouching = true;
            player = other.gameObject.GetComponent<PlayerController>();
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
            player = other.gameObject.GetComponent<PlayerController>();
            player.IsClimbing = false;
            if (instructions != null && !hideInstructions)
            {
                instructions.characterSize = 0;
            }
        }
    }
}

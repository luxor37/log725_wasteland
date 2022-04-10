using Player;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool _areTouching;
    private PlayerController player;
    public bool HideInstructions = false;
    public TextMesh Instructions;

    void Start()
    {
        if (Instructions != null)
        {
            Instructions.characterSize = 0;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up)
        {
            player.IsClimbing = true;
            player.LadderAngle = 180;
        }

        if (InputController.HorizontalDirection != HorizontalDirection.Idle || InputController.IsJumping)
        {
            player.IsClimbing = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = true;
        player = other.gameObject.GetComponent<PlayerController>();
        if (Instructions != null)
        {
            Instructions.characterSize = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Character") return;

        _areTouching = false;
        player = other.gameObject.GetComponent<PlayerController>();
        player.IsClimbing = false;
        if (Instructions != null && !HideInstructions)
        {
            Instructions.characterSize = 0;
        }
    }
}

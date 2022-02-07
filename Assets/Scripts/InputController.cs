using UnityEngine;

public enum Direction { Left, Right, Iddle }

public class InputController : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float controllerDeadZone = 0.2f;
    
    [HideInInspector]
    public static float HorizontalAxis = 0f;

    [HideInInspector]
    public static float VerticalAxis = 0f;
    
    public static Direction Direction;

    [HideInInspector]
    public static bool IsJumping  = false;

    [HideInInspector]
    public static bool IsSprinting  = false;

    [HideInInspector]
    public static bool IsAttacking = false;

    void Update()
    {
        HorizontalAxis = GetAxis("Horizontal");
        VerticalAxis = GetAxis("Vertical");
        IsJumping = Input.GetButtonDown("Jump");
        Direction = GetHorizontalDirection(HorizontalAxis);
    }

    private float GetAxis(string axisName){
        if(Input.GetAxis(axisName) > controllerDeadZone || Input.GetAxis(axisName) < controllerDeadZone){
            return Input.GetAxis(axisName);
        }

        return 0f;
    }

    private Direction GetHorizontalDirection(float horizontalAxis){
        if(horizontalAxis > 0f){
            return Direction.Left;
        }
        else if(horizontalAxis < 0f){
            return Direction.Right;
        }

        return Direction.Iddle;
    }
}

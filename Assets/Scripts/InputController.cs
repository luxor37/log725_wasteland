using UnityEngine;

public enum HorizontalDirection { Left, Right, Iddle }
public enum VerticalDirection { Up, Down, Iddle }

public class InputController : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float controllerDeadZone = 0.2f;
    public static float HorizontalAxis = 0f;
    public static float VerticalAxis = 0f;
    public static HorizontalDirection HorizontalDirection;
    public static VerticalDirection VerticalDirection;
    public static bool IsJumping = false;
    public static bool IsSprinting = false;
    public static bool IsAttacking = false;
    public static bool IsPausing = false;

    void Update()
    {
        HorizontalAxis = GetAxis("Horizontal");
        VerticalAxis = GetAxis("Vertical");
        IsJumping = Input.GetButtonDown("Jump");
        HorizontalDirection = GetHorizontalDirection(HorizontalAxis);
        VerticalDirection = GetVerticalDirection(VerticalAxis);
        IsPausing = Input.GetButtonDown("Menu");
    }

    private float GetAxis(string axisName)
    {
        if (Input.GetAxis(axisName) > controllerDeadZone || Input.GetAxis(axisName) < controllerDeadZone)
        {
            return Input.GetAxis(axisName);
        }

        return 0f;
    }

    private HorizontalDirection GetHorizontalDirection(float horizontalAxis)
    {
        if (horizontalAxis > 0f)
        {
            return HorizontalDirection.Right;
        }
        else if (horizontalAxis < 0f)
        {
            return HorizontalDirection.Left;
        }

        return HorizontalDirection.Iddle;
    }

    private VerticalDirection GetVerticalDirection(float verticalAxis)
    {
        if (verticalAxis > 0f)
        {
            return VerticalDirection.Up;
        }
        else if (verticalAxis < 0f)
        {
            return VerticalDirection.Down;
        }

        return VerticalDirection.Iddle;
    }
}

using System;
using UnityEngine;

public enum HorizontalDirection { Left, Right, Iddle }
public enum VerticalDirection { Up, Down, Iddle }

public class InputController : MonoBehaviour
{
    public static bool disableControls = false;

    [Range(0.1f, 0.9f)]
    public float controllerDeadZone = 0.2f;
    public static float HorizontalAxis = 0f;
    public static float VerticalAxis = 0f;
    public static HorizontalDirection HorizontalDirection;
    public static VerticalDirection VerticalDirection;
    public static bool IsJumping = false;
    public static bool IsSprinting = false;
    public static bool IsAttacking = false;
    public static bool IsShielding = false;
    public static bool IsPausing = false;
    public static int AttackType = 0;
    public static bool IsUsingItem1 = false;
    public static bool IsUsingItem2 = false;
    public static bool IsInteracting = false;

    public static bool IsCharacterChanging = false;

    void Start(){
        disableControls = false;
    }

    void Update()
    {
        if (!disableControls)
        {
            HorizontalAxis = GetAxis("Horizontal");
            VerticalAxis = GetAxis("Vertical");
            HorizontalDirection = GetHorizontalDirection(HorizontalAxis);
            VerticalDirection = GetVerticalDirection(VerticalAxis);

            IsJumping = Input.GetButtonDown("Jump");
            IsAttacking = Input.GetButton("Attack");
            
            IsPausing = Input.GetButtonDown("Menu");
            IsCharacterChanging = Input.GetButtonDown("Switch");

            AttackType += Convert.ToInt32(Input.GetButtonDown("WeaponChange"));

            IsShielding = Input.GetButtonDown("Shield");
            IsInteracting = Input.GetButtonDown("Interact");

            IsUsingItem1 = Input.GetButtonDown("UseItem1");
            IsUsingItem2 = Input.GetButtonDown("UseItem2");
        }
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

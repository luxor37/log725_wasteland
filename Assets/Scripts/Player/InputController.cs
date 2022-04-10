using UnityEngine;
using static PersistenceManager;

public enum HorizontalDirection { Left, Right, Idle }
public enum VerticalDirection { Up, Down, Idle }

public enum AttackTypeEnum { Melee = 0, Ranged = 1 };

public class InputController : MonoBehaviour
{
    public static bool disableControls = false;

    [Range(0.1f, 0.9f)]
    public float ControllerDeadZone = 0.2f;
    public static float HorizontalAxis = 0f;
    public static float VerticalAxis = 0f;
    public static HorizontalDirection HorizontalDirection;
    public static VerticalDirection VerticalDirection;
    public static bool IsJumping = false;
    public static bool IsSprinting = false;
    public static bool IsAttacking = false;
    public static bool IsShielding = false;
    public static bool IsPausing = false;
    public static AttackTypeEnum AttackType = AttackTypeEnum.Melee;
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

            HorizontalAxis = IsAttacking && (AttackType == AttackTypeEnum.Melee || ActiveCharacter == ActiveCharacterEnum.character2) ? 0 : GetAxis("Horizontal");
            VerticalAxis = GetAxis("Vertical");

            HorizontalDirection = GetHorizontalDirection(HorizontalAxis);
            VerticalDirection = GetVerticalDirection(VerticalAxis);

            IsJumping = Input.GetButtonDown("Jump");
            IsAttacking = Input.GetButton("Attack");
            
            IsPausing = Input.GetButtonDown("Menu");
            IsCharacterChanging = Input.GetButtonDown("Switch");

            if(Input.GetButtonDown("MeleeWeapon"))
                AttackType = AttackTypeEnum.Melee;
            if(Input.GetButtonDown("RangedWeapon"))
                AttackType = AttackTypeEnum.Ranged;

            IsShielding = Input.GetButtonDown("Shield");
            IsInteracting = Input.GetButtonDown("Interact");

            IsUsingItem1 = Input.GetButtonDown("UseItem1");
            IsUsingItem2 = Input.GetButtonDown("UseItem2");
        }
    }

    private float GetAxis(string axisName)
    {
        if (Input.GetAxis(axisName) > ControllerDeadZone || Input.GetAxis(axisName) < ControllerDeadZone)
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

        return HorizontalDirection.Idle;
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

        return VerticalDirection.Idle;
    }
}

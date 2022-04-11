using UnityEngine;
using static PersistenceManager;

public enum HorizontalDirection { Left, Right, Idle }
public enum VerticalDirection { Up, Down, Idle }
public enum AttackTypeEnum { Melee = 0, Ranged = 1 };

public class InputController : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float ControllerDeadZone = 0.2f;
    public static float HorizontalAxis;
    public static float VerticalAxis;
    public static HorizontalDirection HorizontalDirection;
    public static VerticalDirection VerticalDirection;
    public static bool IsJumping, IsSprinting, IsAttacking, IsShielding, IsPausing, IsUsingItem1, 
        IsUsingItem2, IsInteracting , DisableControls, IsCharacterChanging;
    public static AttackTypeEnum AttackType = AttackTypeEnum.Melee;

    private static bool _isUsingItem2Continuous, _isUsingItem1Continuous, _isInteractingContinuous = false;

    void Start(){
        DisableControls = false;
    }

    void Update()
    {
        if (DisableControls) return;
        HorizontalAxis = IsAttacking && (AttackType == AttackTypeEnum.Melee || ActiveCharacter == ActiveCharacterEnum.Character2) ? 0 : GetAxis("Horizontal");
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
        
        if (!IsInteracting && !_isInteractingContinuous)
        {
            IsInteracting = Input.GetButtonDown("Interact") || Input.GetAxis("Interact") > 0f;
            _isInteractingContinuous = IsInteracting;
        }
        else if (_isInteractingContinuous && Input.GetAxis("Interact") > 0f)
        {
            IsInteracting = false;
        }
        else
        {
            IsInteracting = false;
            _isInteractingContinuous = false;
        }


        if (!IsUsingItem1 && !_isUsingItem1Continuous)
        {
            IsUsingItem1 = Input.GetButtonDown("UseItem1") || Input.GetAxis("UseItem1") < 0f;
            _isUsingItem1Continuous = IsUsingItem1;
        }
        else if (_isUsingItem1Continuous && Input.GetAxis("UseItem1") < 0f)
        {
            IsUsingItem1 = false;
        }
        else
        {
            IsUsingItem1 = false;
            _isUsingItem1Continuous = false;
        }


        if (!IsUsingItem2 && !_isUsingItem2Continuous)
        {
            IsUsingItem2 = Input.GetButtonDown("UseItem2") || Input.GetAxis("UseItem2") > 0f;
            _isUsingItem2Continuous = IsUsingItem2;
        }
        else if (_isUsingItem2Continuous && Input.GetAxis("UseItem2") > 0f)
        {
            IsUsingItem2 = false;
        }
        else
        {
            IsUsingItem2 = false;
            _isUsingItem2Continuous = false;
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

    private static HorizontalDirection GetHorizontalDirection(float horizontalAxis)
    {
        if (horizontalAxis > 0f)
        {
            return HorizontalDirection.Right;
        }
        if (horizontalAxis < 0f)
        {
            return HorizontalDirection.Left;
        }

        return HorizontalDirection.Idle;
    }

    private static VerticalDirection GetVerticalDirection(float verticalAxis)
    {
        if (verticalAxis > 0f)
        {
            return VerticalDirection.Up;
        }
        if (verticalAxis < 0f)
        {
            return VerticalDirection.Down;
        }

        return VerticalDirection.Idle;
    }

    private void GameManagerRegisterInput()
    {
        

    }
}

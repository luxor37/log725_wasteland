using System;
using UnityEngine;
using UnityEngine.XR;
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

    private TextMesh instructions;

    void Start(){
        DisableControls = false;
        instructions = GameObject.Find("interactPrompt").GetComponent<TextMesh>();
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
        
        (IsInteracting, _isInteractingContinuous) = GetControllerAxisButtonInput(
            IsInteracting,
            (Input.GetButtonDown("Interact") || Input.GetAxis("Interact") > 0f),
            _isInteractingContinuous);

        (IsUsingItem1, _isUsingItem1Continuous) = GetControllerAxisButtonInput(
            IsUsingItem1,
            Input.GetButtonDown("UseItem1") || Input.GetAxis("UseItem1") < 0f,
            _isUsingItem1Continuous);

        (IsUsingItem2, _isUsingItem2Continuous) = GetControllerAxisButtonInput(
            IsUsingItem2, 
            Input.GetButtonDown("UseItem2") || Input.GetAxis("UseItem2") > 0f,
            _isUsingItem2Continuous);
        
        if (instructions != null)
        {
            instructions.text = _mState switch
            {
                EInputState.MouseKeyboard => "'F' to interact",
                EInputState.Controller => "'d-pad up' to interact",
                _ => instructions.text
            };
        }
    }

    private Tuple<bool, bool> GetControllerAxisButtonInput(bool isInteracting, bool input, bool isInteractingContinuous)
    {
        if (!isInteracting && !isInteractingContinuous)
        {
            isInteracting = input;
            isInteractingContinuous = isInteracting;
        }
        else if (isInteractingContinuous && input)
        {
            isInteracting = false;
        }
        else
        {
            isInteracting = false;
            isInteractingContinuous = false;
        }

        return new Tuple<bool, bool>(isInteracting, isInteractingContinuous);
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



    //https://answers.unity.com/questions/131899/how-do-i-check-what-input-device-is-currently-beei.html
    //https://answers.unity.com/storage/temp/134371-xbox-one-controller-unity-windows-macos.jpg

    public enum EInputState
    {
        MouseKeyboard,
        Controller
    };
    private EInputState _mState = EInputState.MouseKeyboard;

    private void OnGUI()
    {
        switch (_mState)
        {
            case EInputState.MouseKeyboard:
                if (IsControllerInput())
                {
                    _mState = EInputState.Controller;
                    Debug.Log("DREAM - JoyStick being used");
                }
                break;
            case EInputState.Controller:
                if (IsMouseKeyboard())
                {
                    _mState = EInputState.MouseKeyboard;
                    Debug.Log("DREAM - Mouse & Keyboard being used");
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public EInputState GetInputState()
    {
        return _mState;
    }
    
    private static bool IsMouseKeyboard()
    {
        // mouse & keyboard buttons
        return Event.current.isKey ||
               Event.current.isMouse;
    }

    private static bool IsControllerInput()
    {
        // joystick buttons
        return Input.GetKey(KeyCode.Joystick1Button0) ||
               Input.GetKey(KeyCode.Joystick1Button1) ||
               Input.GetKey(KeyCode.Joystick1Button2) ||
               Input.GetKey(KeyCode.Joystick1Button3) ||
               Input.GetKey(KeyCode.Joystick1Button4) ||
               Input.GetKey(KeyCode.Joystick1Button5) ||
               Input.GetKey(KeyCode.Joystick1Button6) ||
               Input.GetKey(KeyCode.Joystick1Button7) ||
               Input.GetKey(KeyCode.Joystick1Button8) ||
               Input.GetKey(KeyCode.Joystick1Button9) ||
               Input.GetKey(KeyCode.Joystick1Button10) ||
               Input.GetKey(KeyCode.Joystick1Button11) ||
               Input.GetKey(KeyCode.Joystick1Button12) ||
               Input.GetKey(KeyCode.Joystick1Button13) ||
               Input.GetKey(KeyCode.Joystick1Button14) ||
               Input.GetKey(KeyCode.Joystick1Button15) ||
               Input.GetKey(KeyCode.Joystick1Button16) ||
               Input.GetKey(KeyCode.Joystick1Button17) ||
               Input.GetKey(KeyCode.Joystick1Button18) ||
               Input.GetKey(KeyCode.Joystick1Button19);
    }
}

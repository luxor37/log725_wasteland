using CustomAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _controller;
    private int _isRunningHash;
    private int _isJumpingHash;
    private float _moveSpeed;
    private Vector3 _moveDirection;
    private Quaternion _rotation = Quaternion.identity;
    private bool isGrounded = true;


    //Rotation speed
    [ReadOnly]
    public float rotationSpeed = 10f;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _isRunningHash = Animator.StringToHash("isRuning");
    }

    //So it is always after InputController's Update
    void LateUpdate()
    {
        isGrounded = _controller.isGrounded;
        
        PlayerAnimationController.Animate(_animator, isGrounded);

        transform.rotation = PlayerMovementController.GetRotation(transform.rotation);
    }

    // override this so that movement doesn't get screwed
    void OnAnimatorMove()
    {
        Vector3 desiredMovement = PlayerMovementController.GetMovement(moveSpeed, isGrounded, jumpForce, gravityScale);
        _controller.Move(desiredMovement * Time.deltaTime);
    }
}

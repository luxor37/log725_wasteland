using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right, Iddle }

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public Vector2 ZLimit = new Vector2(-3f, 2.4f);
    public GameObject gameMenu;
    public Animator animator;


    [HideInInspector]
    public Direction Direction;


    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _horizontalAxis;
    private float _verticalAxis;
    private bool _isJumpingInput;
    private bool _isSprintingInput;
    private bool _isAttackingInput;
    private bool _gamePaused = false;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _isJumpingInput = Input.GetButtonDown("Jump");
        _isSprintingInput = Input.GetButton("Sprint");
        _isAttackingInput = Input.GetButtonDown("Attack");

        OtherInputs();

        if (!_gamePaused)
        {
            DetectMoveInputDirection();
            Movement();
            SetInvisibleLimits();
            Attack();
        }
    }

    void DetectMoveInputDirection()
    {
        if (_horizontalAxis > 0f)
            Direction = Direction.Right;
        else if (_horizontalAxis < 0f)
            Direction = Direction.Left;
        else
            Direction = Direction.Iddle;
    }

    void SetInvisibleLimits()
    {
        if (transform.position.z <= ZLimit.x)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, ZLimit.x);
        }
        if (transform.position.z >= ZLimit.y)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, ZLimit.y);
        }
    }

    void Movement()
    {
        float speed = _isSprintingInput ? moveSpeed * 0.1f : moveSpeed;

        animator.SetBool("isRunning", _isSprintingInput);

        _moveDirection = new Vector3(
            _horizontalAxis * speed,
            _moveDirection.y,
            _verticalAxis * speed);

        //Character model orientation based on movement
        if (_horizontalAxis != 0 || _verticalAxis != 0)
        {
            var desiredMoveDirection = Camera.main.transform.forward * _verticalAxis + Camera.main.transform.right * _horizontalAxis;
            if (_isSprintingInput)
            {
                Vector3 desiredForward = Vector3.RotateTowards(transform.forward, desiredMoveDirection, 1 * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(desiredForward);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            }

        }

        var grounded = _controller.isGrounded;

        if (_controller.isGrounded && _isJumpingInput)
        {
            _moveDirection.y = jumpForce;
            grounded = false;
        }

        //setting animator conditions to determine which animation to use
        animator.SetFloat("Speed", _controller.velocity.magnitude);
        animator.SetBool("isGrounded", grounded);

        //applying gravity
        _moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

        //applying move with deltatime so that movement is not tied to framerate
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    void OtherInputs()
    {

        if (Input.GetButtonDown("Menu") && gameMenu != null)
        {
            _gamePaused = !_gamePaused;
            gameMenu.SetActive(_gamePaused);

            Time.timeScale = _gamePaused ? 0 : 1;
        }
    }

    void Attack()
    {
        animator.SetBool("isPunching", _isAttackingInput);

        // if(_isAttackingInput){

        // }
    }
}

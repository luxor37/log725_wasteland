using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right, Iddle }

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _controller;
    private int _isRunningHash;
    private int _isJumpingHash;
    private float _moveSpeed;
    private Vector3 _moveDirection;
    private float _horizontal;
    private float _vertical;
    private bool _isJumpingInput;
    private Quaternion _rotation = Quaternion.identity;
    private bool _isSprintingInput = false;

    [HideInInspector]
    public Direction Direction;

    [HideInInspector]
    public bool _gamePaused = false;

    //Rotation speed
    public float rotationSpeed = 10f;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public GameObject gameMenu;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _isRunningHash = Animator.StringToHash("isRuning");
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _isJumpingInput = Input.GetButtonDown("Jump");
    }

    void Update()
    {
        float pZ = transform.position.z;
        _horizontal = -Input.GetAxis("Horizontal");
        // _vertical = Input.GetAxis("Vertical");
        _isJumpingInput = Input.GetButtonDown("Jump");
        //Horizontal movement check
        bool hasHorizontal = !Mathf.Approximately(_horizontal, 0.0f);
        _animator.SetBool(_isRunningHash, hasHorizontal);

        Movement();
        Rotation();
        Jump();

        
        gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, pZ);
    }

    // override this so that movement doesn't get screwed
    void OnAnimatorMove()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }


    void Movement()
    {
        float speed = _isSprintingInput ? moveSpeed * 2f : moveSpeed;

        //User input to Movement
        _moveDirection.x = _horizontal * speed;
    }

    void Jump()
    {
        var grounded = _controller.isGrounded;
        if (grounded)
        {
            _animator.SetBool("isJumping", false);
            _moveDirection.y = 0f;
            
            if (_isJumpingInput)
            {
                _moveDirection.y += jumpForce;
                // grounded = false;
                _animator.SetBool("isJumping", true);
            }

        }

        //applying gravity
        _moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        _animator.SetBool("isGrounded", grounded);
    }

    void Rotation()
    {
        if (!Mathf.Approximately(_horizontal, 0f))
        {
            var desiredMoveDirection = Camera.main.transform.right * -_horizontal;
            //_moveDirection += Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f).eulerAngles;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            // using below because of rigid body issue
            transform.forward = desiredMoveDirection;
        }
    }
}

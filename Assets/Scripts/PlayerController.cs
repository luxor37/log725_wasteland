using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Iddle
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;


    // [HideInInspector] 
    public Direction Direction;



    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _horizontal;
    private float _vertical;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        
        DetectMoveInputDirection();
        Movement();
        SetInvisibleLimits();
    }

    void DetectMoveInputDirection()
    {
        if (_horizontal > 0f)
            Direction = Direction.Right;
        else if (_horizontal < 0f)
            Direction = Direction.Left;
        else
            Direction = Direction.Iddle;
    }

    void SetInvisibleLimits()
    {
        if (transform.position.z <= -3f)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -3f);
        }
        if (transform.position.z >= 2.4f)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 2.4f);
        }
    }

    void Movement()
    {
        float speed = moveSpeed;
        if (Input.GetButton("Sprint"))
        {
            speed = moveSpeed * 2;
        }

        _moveDirection = new Vector3(
            _horizontal * speed,
            _moveDirection.y,
            _vertical * speed);

        if (_horizontal != 0 || _vertical != 0)
        {
            var desiredMoveDirection = Camera.main.transform.forward * _vertical + Camera.main.transform.right * _horizontal;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
        }

        if (_controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            _moveDirection.y = jumpForce;
        }

        //applying gravity
        _moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

        //applying move with deltatime so that movement is not tied to framerate
        _controller.Move(_moveDirection * Time.deltaTime);
    }
}

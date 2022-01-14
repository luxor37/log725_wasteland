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
    private MeshCollider _ground;

    private Vector3 _moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        // _rigidBody = this.GetComponent<Rigidbody>();
        // _collider = this.GetComponent<CapsuleCollider>();
        _ground = GameObject.Find("Ground_plane").GetComponent<MeshCollider>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectMoveInputDirection();
        SetInvisibleLimits();
        MoveAndJump();
    }

    void DetectMoveInputDirection()
    {
        if (Input.GetAxis("Horizontal") > 0f)
            Direction = Direction.Right;
        else if (Input.GetAxis("Horizontal") < 0f)
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

    void MoveAndJump()
    {
        float speed = moveSpeed;
        if(Input.GetButton("Sprint")){
            speed = moveSpeed * 2; 
        }

        _moveDirection = new Vector3(
            Input.GetAxis("Horizontal") * speed,
            _moveDirection.y,
            Input.GetAxis("Vertical") * speed);

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

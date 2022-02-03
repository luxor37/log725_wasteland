using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private int isRunningHash;
    private int isJumpingHash;

    private float moveSpeed;
    

    //Character direction 
    private Vector3 _CharacterMovement; 
    //User input direction 
    private float horizontal;
    private float vertical;
    
    //Character Rotation 
    Quaternion _Rotation = Quaternion.identity;
    //Rotation speed
    public float rotationSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3f;
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRuning");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = -Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        //User input to Movement
        _CharacterMovement.Set(horizontal,0,0);
        _CharacterMovement.Normalize();
        
        //Horizontal movement check
        bool hasHorizontal = !Mathf.Approximately(horizontal, 0.0f);
        _animator.SetBool(isRunningHash, hasHorizontal);
        
        //Use 3d vector as character backward direction
        Vector3 desiredForward =
            Vector3.RotateTowards(transform.forward, _CharacterMovement, rotationSpeed * Time.deltaTime, 0f);

        _Rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        //Character Movement distance = animation's every 0.02sec Movement distance 
        _rigidbody.MovePosition(_rigidbody.position + _CharacterMovement * _animator.deltaPosition.magnitude * 2);
        //Rotate character
        _rigidbody.MoveRotation(_Rotation);
    }
}

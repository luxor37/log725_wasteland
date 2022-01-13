using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector3(
                rigidbody.velocity.x,
                rigidbody.velocity.y + jumpForce,
                rigidbody.velocity.z);
        }
    }

    void Move()
    {
        rigidbody.velocity = new Vector3(
            Input.GetAxis("Horizontal") * moveSpeed,
            rigidbody.velocity.y,
            Input.GetAxis("Vertical") * moveSpeed);
    }
}

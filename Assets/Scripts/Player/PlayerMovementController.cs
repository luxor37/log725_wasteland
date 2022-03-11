using System;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Video;

namespace Player
{
    //Set CharacterController: y:0.8 and Height: 1.5
    public class PlayerMovementController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _controller;
        
        [Header("moving parameter")]
        public float moveSpeed = 5f;
        public float currentSpeed = 0f;
        [SerializeField] private Vector3 _desiredMoveDirection;
        [SerializeField] private Vector3 _desiredMovement;

        [Header("Surronding check parameter")] [SerializeField]
        private bool _isTouchWall = false;
        [SerializeField] private bool _isGrounded = false;
        [SerializeField] private bool _isTouchEdge = false;
        private bool canClimbEdge = false;
        private bool edgeDectected = false;
        public float wallCheckDistance = 0.1f;
        public float groundCheckDistance = 0.1f;
        public float ForwardgroundCheckDistance = 0.1f;
        public float BackgroundCheckDistance = 0.1f;
        public float edgeCheckDistance = 0.1f;
        public Transform edgeCheckPoint;
        public Transform WallCheckPoint;
        public Transform ForwardGroundCheckPoint;
        public Transform BackGroundCheckPoint;
        private Vector3 edgePosBottom;
        private Vector3 edgePosTop;
        public float edgeClimbXoffset1 = 0f;
        public float edgeClimbYoffset1 = 0f;
  

        [Header("Jump parameter")] public float maxJumpHeight = 1f;
        public float initialJumpVelocity = 3f;
        public float maxJumpTime = 0.5f;
        private float gravity = 9.81f;
        public float gravityScale = 1f;
        private bool canJump = true;
        private bool isJumping = false;

        [Header("Climbing parameter")] 
        public float climbingSpeed = 2f;
        [SerializeField]public bool isClimbing = false;
        public Transform ClimbTatget;


        private BoxCollider _collider;
        private float timer = 0f;
        public LayerMask GroundLayer;

        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<BoxCollider>();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            currentSpeed = moveSpeed;
        }

        //So it is always after InputController's Update
        void LateUpdate()
        {
            if (!PauseMenu.isGamePaused)
            {
                transform.rotation = GetRotation(transform.rotation); //Rotate to Right or Left
                //Character cannot depass world limit
                transform.position = VerifyWorldLimits(transform.position);

                isGroundCheck(); //Check and setup if Character on land
                isWallCheck(); //Check if character forward has a wall

                //Find movement
  
                _desiredMovement = GetMovement();
                
                Climb();
                jump();
                _controller.Move(_desiredMovement * Time.deltaTime); //Final move
                if (isClimbing)
                {
                    _controller.gameObject.transform.position = new Vector3(ClimbTatget.position.x,
                        transform.position.y, transform.position.z);
                }
                

            }

            
        }

        private void isGroundCheck()
        {
            _isGrounded = Physics.Raycast(_collider.bounds.center, Vector3.down,
                              _collider.bounds.extents.y + groundCheckDistance, GroundLayer) ||
                          Physics.Raycast(ForwardGroundCheckPoint.position, Vector3.down,
                              ForwardgroundCheckDistance, GroundLayer) ||
                          Physics.Raycast(BackGroundCheckPoint.position, Vector3.down,
                              BackgroundCheckDistance, GroundLayer);
            _animator.speed = 1;
            _animator.SetBool("isGrounded", _isGrounded);
        }

        private void isWallCheck()
        {
            _isTouchWall = Physics.Raycast(WallCheckPoint.position, transform.forward,
                _collider.bounds.extents.x + wallCheckDistance, GroundLayer);
        }


        void jump()
        {
            float timeToApex = maxJumpTime / 2;
            gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
            if (Input.GetButtonDown("Jump") && _isGrounded && !isJumping)
            {
                isJumping = true;
                _desiredMovement.y = initialJumpVelocity;
                _animator.SetTrigger("Jump");
            }
            else if (Input.GetButtonDown("Jump") && !_isGrounded)
            {
                isJumping = true;
                if (Mathf.Abs(InputController.HorizontalAxis) > 0)
                {
                    _desiredMovement.y = initialJumpVelocity;
                    _animator.SetTrigger("Jump");
                    return;
                }
                _animator.SetTrigger("Jump");
                transform.forward = Vector3.left;

            }
            else if (isJumping && _isGrounded)
            {
                isJumping = false;
            }

        }
        void Climb()
        {
            if (isClimbing && !InputController.IsAttacking)
            {
                if (isJumping)
                {
                    isClimbing = false;
                    _animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
                    _animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
                    return;
                }
                _desiredMovement = GetClimbingMovement(climbingSpeed);
                transform.localEulerAngles = new Vector3(0, 180, 0);
                if(_isGrounded)
                {
                    transform.forward = Vector3.left;
                    isClimbing = false;
                }
                _animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
                _animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
                if (isClimbing)
                {
                    if (InputController.VerticalDirection == VerticalDirection.Iddle)
                        _animator.speed = 0;
                    else
                        _animator.speed = 1;
                }
                else
                    _animator.speed = 1;

            }
            
        }

        public Quaternion GetRotation(Quaternion currentRotation)
        {
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0f))
            { 
                _desiredMoveDirection = Camera.main.transform.right * InputController.HorizontalAxis;
                return Quaternion.LookRotation(_desiredMoveDirection);
            }
            return currentRotation;
        }

        public static Vector3 VerifyWorldLimits(Vector3 playerPosition)
        {
            if (playerPosition.y > WorldConfig.YMax)
            {
                playerPosition.y = WorldConfig.YMax;
            }

            if (playerPosition.y < WorldConfig.YMin)
            {
                playerPosition.y = WorldConfig.YMin;
            }

            if (playerPosition.x > WorldConfig.XMax)
            {
                playerPosition.x = WorldConfig.XMax;
            }

            if (playerPosition.x < WorldConfig.XMin)
            {
                playerPosition.x = WorldConfig.XMin;
            }

            return playerPosition;
        }


        public Vector3 GetMovement()
        {
            var desiredMovement = new Vector3(
                GetHorizontalMovement(),
                GetVerticalMovement(),
                0f);
            return desiredMovement;
        }

        private float GetHorizontalMovement()
        {
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0.0f) && _isGrounded)
            {
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
            float speed = InputController.IsSprinting ? currentSpeed * 2f : currentSpeed;
            return -InputController.HorizontalAxis * speed;
        }

        private float GetVerticalMovement()
        {
            var yMovement = _desiredMovement.y;

            if (_isGrounded) //on land
            {
                yMovement = 0f;
            }
            else if (!_isGrounded && !isClimbing) //in air
            {
                yMovement += gravity * gravityScale * Time.deltaTime;
            }

            return yMovement;

        }

        public Vector3 GetClimbingMovement(float climbingSpeed)
        {
            var desiredMovement = new Vector3(
                0,
                InputController.VerticalAxis * climbingSpeed,
                0f);

            return desiredMovement;
        }

        public void disableMovement()
        {
            currentSpeed = 0;
        }
        
        public void enableMovement()
        {
            currentSpeed = moveSpeed;
        }

        public bool IsGrounded
        {
            get => _isGrounded;
        }
    }
}
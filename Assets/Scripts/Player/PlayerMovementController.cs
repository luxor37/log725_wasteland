using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Player
{
    //Set CharacterController: y:0.8 and Height: 1.5
    public class PlayerMovementController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _controller;
        private Vector3 _desiredMovement;

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

        [Header("Climbing parameter")] public float moveSpeed = 5f;
        public float climbingSpeed = 2f;
        public bool isClimbing = false;
        private bool isLadder;

        private Transform ladderPosition;
        private BoxCollider _collider;
        
        public LayerMask GroundLayer;

        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<BoxCollider>();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
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
                isClimbEdgeCheck(); //Check if character forward has an edge

                //Find movement
                _desiredMovement = GetMovement();
                Climb();
                jump();
                
                _controller.Move(_desiredMovement * Time.deltaTime); //Final move

            }
        }

        private void isGroundCheck()
        {
            Debug.Log(Physics.Raycast(ForwardGroundCheckPoint.position, Vector3.down,
                ForwardgroundCheckDistance, GroundLayer) );
            _isGrounded = Physics.Raycast(_collider.bounds.center, Vector3.down,
                              _collider.bounds.extents.y + groundCheckDistance, GroundLayer) ||
                          Physics.Raycast(ForwardGroundCheckPoint.position, Vector3.down,
                              ForwardgroundCheckDistance, GroundLayer) ||
                          Physics.Raycast(BackGroundCheckPoint.position, Vector3.down,
                              BackgroundCheckDistance, GroundLayer);
            
            _animator.SetBool("isGrounded", _isGrounded);
        }

        private void isWallCheck()
        {
            _isTouchWall = Physics.Raycast(WallCheckPoint.position, transform.forward,
                _collider.bounds.extents.x + wallCheckDistance, GroundLayer);
        }

        private void isClimbEdgeCheck()
        {
            if (edgeCheckPoint != null && _isTouchWall)
            {
                _isTouchEdge = !Physics.Raycast(edgeCheckPoint.position, transform.forward,
                    edgeCheckDistance, GroundLayer);
            }

            if (_isTouchEdge && !_isGrounded && _isTouchWall)
            {
                edgeDectected = true;
            }
        }

        private void playClimbEdge()
        {
            if (edgeDectected)
            {
                _desiredMovement = new Vector3(0, 0, 0);
                edgePosTop = edgeCheckPoint.position + new Vector3(-edgeClimbXoffset1,edgeClimbYoffset1,0);
                _animator.SetTrigger("ClmbEdge");
               
                
            }
        }

        public void ClimbEdge()
        { 
            _controller.gameObject.transform.position = edgePosTop;
            edgeDectected = false;
            canClimbEdge = false;
        }

        void setupJumpVariable()
        {
            float timeToApex = maxJumpTime / 2;
            gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        }

        void jump()
        {
            setupJumpVariable();
            if (Input.GetButtonDown("Jump") && _isGrounded && !isJumping)
            {
                isJumping = true;
                _desiredMovement.y = initialJumpVelocity;
                _animator.SetTrigger("Jump");
            }
            else if (isJumping && _isGrounded)
            {
                isJumping = false;
            }

        }

        void Climb()
        {
            if (isLadder)
            {
                if (!isClimbing && Mathf.Abs(InputController.VerticalAxis) > 0.0f) //first touch with ladder
                {
                    isClimbing = true;
                    transform.LookAt(ladderPosition);
                    _animator.SetTrigger("Climb");
                    _animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
                    _animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
                    _desiredMovement = GetClimbingMovement(climbingSpeed);
                }
                else if (isClimbing) // while Climbing
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    if (_isGrounded)
                    {
                        isClimbing = false;
                        _animator.SetBool("IsClimbing", isClimbing);
                        _animator.SetBool("IsClimbingDown", isClimbing);
                        return;
                    }
                    _desiredMovement = GetClimbingMovement(climbingSpeed); 
                    _animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
                    _animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
                    
                }

            }

        }

        public static Quaternion GetRotation(Quaternion currentRotation)
        {
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0f))
            {
                var desiredMoveDirection = Camera.main.transform.right * InputController.HorizontalAxis;
                return Quaternion.LookRotation(desiredMoveDirection);
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
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0.0f))
            {
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
            float speed = InputController.IsSprinting ? moveSpeed * 2f : moveSpeed;
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

        public static Vector3 GetClimbingMovement(float climbingSpeed)
        {
            var desiredMovement = new Vector3(
                0f,
                InputController.VerticalAxis * climbingSpeed,
                0f);

            return desiredMovement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                isLadder = true;
                ladderPosition = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                isLadder = false;
                ladderPosition = null;
            }
        }
    }
}
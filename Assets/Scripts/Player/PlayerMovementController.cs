using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 _desiredMovement;
        private static bool _isGrounded = false;
        
        public float jumpForce = 10f;
        public float gravityScale = 1f;
        private static bool isJumping = false;
        

        private PlayerCharacter _character;
        private Animator _animator;
        
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _character = this.gameObject.GetComponent<PlayerCharacter>();
        }

        //So it is always after InputController's Update
        void LateUpdate()
        {
            if (!PauseMenu.isGamePaused)
            {
                transform.rotation = GetRotation(transform.rotation);

                _desiredMovement = GetMovement(_desiredMovement.y, _character.movementSpeed, _controller.isGrounded, jumpForce, gravityScale);
                _controller.Move(_desiredMovement * Time.deltaTime);
                transform.position = VerifyWorldLimits(transform.position);

                bool hasHorizontal = !Mathf.Approximately(InputController.HorizontalAxis, 0.0f);
                _animator.SetBool("isRunning", hasHorizontal);

                if(!_controller.isGrounded && InputController.IsJumping){
                    isJumping = true;
                }
                if(_controller.isGrounded){
                    if(InputController.IsJumping)
                        isJumping = true;
                    isJumping = false;
                }

                _animator.SetBool("isJumping",isJumping);
                _animator.SetBool("isGrounded", _controller.isGrounded);
            }

        }

        public Quaternion GetRotation(Quaternion currentRotation)
        {
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0f))
            {
                var desiredMoveDirection = Camera.main.transform.right * InputController.HorizontalAxis;
                return Quaternion.LookRotation(desiredMoveDirection);
            }
            return currentRotation;
        }

        public Vector3 VerifyWorldLimits(Vector3 playerPosition){
            if(playerPosition.y > WorldConfig.YMax){
                playerPosition.y = WorldConfig.YMax;
            }

            if(playerPosition.y < WorldConfig.YMin){
                playerPosition.y = WorldConfig.YMin;
            }

            if(playerPosition.x > WorldConfig.XMax){
                playerPosition.x = WorldConfig.XMax;
            }

            if(playerPosition.x < WorldConfig.XMin){
                playerPosition.x = WorldConfig.XMin;
            }

            return playerPosition;
        }


        public  Vector3 GetMovement(float y, float moveSpeed, bool isGrounded, float jumpForce, float gravityScale)
        {
            var desiredMovement = new Vector3(
                GetHorizontalMovement(moveSpeed),
                GetVerticalMovement(y, isGrounded, jumpForce, gravityScale),
                0f);

            return desiredMovement;
        }

        private  float GetHorizontalMovement(float moveSpeed)
        {
            float speed = InputController.IsSprinting ? moveSpeed * 2f : moveSpeed;

            //User input to Movement
            return -InputController.HorizontalAxis * speed;
        }

        private  float GetVerticalMovement(float y, bool isGrounded, float jumpForce, float gravityScale)
        {
            var yMovement = y;

            if (isGrounded)
            {
                yMovement = 0f;
                _character.CanJump = true;
            }

            if (InputController.IsJumping && _character.CanJump)
            {
                yMovement = jumpForce;
                _character.CanJump = false;
            }

            if (!isGrounded)
            {
                yMovement += Physics.gravity.y * gravityScale * Time.deltaTime;
            }

            return yMovement;

        }

    }
}
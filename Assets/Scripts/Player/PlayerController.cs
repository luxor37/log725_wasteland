using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    //Set CharacterController: y:0.8 and Height: 1.5
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _controller;
        private Vector3 _desiredMovement;
        private bool _isGrounded = false;
        public float moveSpeed = 5f;
        public float climbingSpeed = 1f;
        public float jumpForce = 10f;
        public float gravityScale = 1f;

        public bool isClimbing = false;
        [HideInInspector]
        public float ladderAngle = 0;

        public Text CoinCounter;

        private PlayerAttack _playerAttack;

        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _playerAttack = GetComponent<PlayerAttack>();
        }

        //So it is always after InputController's Update
        void LateUpdate()
        {
            if(CoinCounter != null){
                CoinCounter.text = PersistenceManager.coins.ToString();
            }

            if (!PauseMenu.isGamePaused)
            {

                transform.rotation = PlayerMovementController.GetRotation(transform.rotation);

                _desiredMovement = PlayerMovementController.GetMovement(_desiredMovement.y, moveSpeed, _controller.isGrounded || isClimbing, jumpForce, gravityScale);

                if (isClimbing)
                {
                    _desiredMovement = PlayerMovementController.GetClimbingMovement(climbingSpeed);
                    transform.localEulerAngles = new Vector3(0, ladderAngle, 0);
                    if(_controller.isGrounded){
                        isClimbing = false;
                    }
                }

                _controller.Move(_desiredMovement * Time.deltaTime);

                transform.position = PlayerMovementController.VerifyWorldLimits(transform.position);

                PlayerAnimationController.Animate(_animator, _controller.isGrounded, isClimbing);
            }

        }

        public bool getIsGrounded()
        {
            return this._isGrounded;
        }

    }
}
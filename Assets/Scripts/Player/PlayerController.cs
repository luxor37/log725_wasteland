using System.Linq;
using Status;
using UnityEngine;
using UnityEngine.UI;
using static ItemController;

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
        public float jumpForce = 5f;
        public float gravityScale = 1f;


        public bool isClimbing = false;
        [HideInInspector]
        public float ladderAngle = 0;

        public Text CoinCounter;

        private PlayerAttack _playerAttack;

        public float shieldCooldown = 3f;
        [HideInInspector]
        public float shieldTimer = -1f;
        [HideInInspector]
        public bool isShielded = false;

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
            if (CoinCounter != null)
            {
                CoinCounter.text = PersistenceManager.coins.ToString();
            }

            if (!PauseMenu.isGamePaused)
            {

                transform.rotation = PlayerMovementController.GetRotation(transform.rotation);

                _desiredMovement = PlayerMovementController.GetMovement(_desiredMovement.y, moveSpeed, _controller.isGrounded || isClimbing, jumpForce, gravityScale);

                HandleShield();

                if (isClimbing)
                {
                    _desiredMovement = PlayerMovementController.GetClimbingMovement(climbingSpeed);
                    transform.localEulerAngles = new Vector3(0, ladderAngle, 0);
                    if (_controller.isGrounded)
                    {
                        isClimbing = false;
                    }
                }

                _controller.Move(_desiredMovement * Time.deltaTime);

                transform.position = PlayerMovementController.VerifyWorldLimits(transform.position);

                PlayerAnimationController.Animate(_animator, _controller.isGrounded, isClimbing);
            }

        }

        private void HandleShield()
        {
            if(shieldTimer == -1f){
                if (InputController.IsShielding)
                {
                    var controller = gameObject.GetComponent<PlayerStatusController>();
                    if (controller != null)
                    {
                        var status = StatusManager.Instance.GetNewStatusObject(StatusEnum.Shield, controller);
                        controller.AddStatus(status);
                    }
                    isShielded = true;
                }
                if(isShielded){
                    var statuses = gameObject.GetComponent<PlayerStatusController>().statuses;

                    if(statuses.OfType<ShieldStatus>().Count() == 0){
                        isShielded = false;
                        shieldTimer = 0f;
                    }
                    
                }
            }
            else
            {
                shieldTimer += Time.deltaTime;
            }

            if (shieldTimer > shieldCooldown)
            {
                shieldTimer = -1f;
            }
        }

        public bool getIsGrounded()
        {
            return this._isGrounded;
        }

    }
}
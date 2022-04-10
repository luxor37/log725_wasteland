using System;
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
        public float MoveSpeed = 5f;
        public float ClimbingSpeed = 1f;
        public float JumpForce = 5f;
        public float GravityScale = 1f;

        [HideInInspector]
        public bool IsClimbing;
        [HideInInspector]
        public float LadderAngle = 0;

        public Text CoinCounter;

        public float ShieldCooldown = 3f;
        [HideInInspector]
        public float ShieldTimer = -1f;
        [HideInInspector]
        public bool IsShielded;

        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        //So it is always after InputController's Update
        void LateUpdate()
        {
            if (CoinCounter != null)
            {
                CoinCounter.text = PersistenceManager.coins.ToString();
            }

            if (PauseMenu.IsGamePaused) return;

            transform.rotation = PlayerMovementController.GetRotation(transform.rotation);

            _desiredMovement = PlayerMovementController.GetMovement(_desiredMovement.y, MoveSpeed, _controller.isGrounded || IsClimbing, JumpForce, GravityScale);

            HandleShield();

            Climbing();

            _controller.Move(_desiredMovement * Time.deltaTime);

            transform.position = PlayerMovementController.VerifyWorldLimits(transform.position);

            PlayerAnimationController.Animate(_animator, _controller.isGrounded, IsClimbing);
        }

        private void Climbing()
        {
            if (IsClimbing)
            {
                _desiredMovement = PlayerMovementController.GetClimbingMovement(ClimbingSpeed);
                transform.localEulerAngles = new Vector3(0, LadderAngle, 0);
                if (_controller.isGrounded)
                {
                    IsClimbing = false;
                }
            }
        }

        private void HandleShield()
        {
            if(Math.Abs(ShieldTimer - (-1f)) <= 0){
                if (InputController.IsShielding)
                {
                    var controller = gameObject.GetComponent<PlayerStatusController>();
                    if (controller != null)
                    {
                        var status = StatusManager.Instance.GetNewStatusObject(StatusEnum.Shield, controller);
                        controller.AddStatus(status);
                    }
                    IsShielded = true;
                }
                if(IsShielded){
                    var statuses = gameObject.GetComponent<PlayerStatusController>().statuses;

                    if(!statuses.OfType<ShieldStatus>().Any()){
                        IsShielded = false;
                        ShieldTimer = 0f;
                    }
                    
                }
            }
            else
            {
                ShieldTimer += Time.deltaTime;
            }

            if (ShieldTimer > ShieldCooldown)
            {
                ShieldTimer = -1f;
            }
        }
    }
}
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _controller;
        private Vector3 _desiredMovement;
        private bool _isGrounded = false;


        public float rotationSpeed = 10f;
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float gravityScale = 1f;

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
            if(!_isGrounded){
                _isGrounded = _controller.isGrounded;
            }
            
            transform.rotation = PlayerMovementController.GetRotation(transform.rotation);

            _desiredMovement = PlayerMovementController.GetMovement(_desiredMovement.y, moveSpeed,_isGrounded, jumpForce, gravityScale);

   
            _controller.Move(_desiredMovement * Time.deltaTime);
            

            if(_desiredMovement.y >= Mathf.Abs(Physics.gravity.y * gravityScale * Time.deltaTime)){
                _isGrounded = false;
            }

            PlayerAnimationController.Animate(_animator, _isGrounded);
        }

        public bool getIsGrounded()
        {
            return this._isGrounded;
        }

    }
}
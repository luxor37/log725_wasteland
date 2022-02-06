using UnityEngine;

namespace Player
{
    public class PlayerMovementController
    {
        public static Quaternion GetRotation(Quaternion currentRotation)
        {
            if (!Mathf.Approximately(InputController.HorizontalAxis, 0f))
            {
                var desiredMoveDirection = Camera.main.transform.right * InputController.HorizontalAxis;
                return Quaternion.Slerp(currentRotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            }
            return currentRotation;
        }

        public static Vector3 GetMovement(float y, float moveSpeed, bool isGrounded, float jumpForce, float gravityScale)
        {
            var desiredMovement = new Vector3(
                GetHorizontalMovement(moveSpeed),
                GetVerticalMovement(y, isGrounded, jumpForce, gravityScale),
                0f);

            return desiredMovement;
        }

        private static float GetHorizontalMovement(float moveSpeed)
        {
            float speed = InputController.IsSprinting ? moveSpeed * 2f : moveSpeed;

            //User input to Movement
            return -InputController.HorizontalAxis * speed;
        }

        private static float GetVerticalMovement(float y, bool isGrounded, float jumpForce, float gravityScale)
        {
            var yMovement = y;

            if (isGrounded)
            {
                yMovement = 0f;
                if (InputController.IsJumping)
                {
                    yMovement = jumpForce;
                }
            }
<<<<<<< HEAD
            else if(!isGrounded || InputController.IsJumping){
                yMovement += Physics.gravity.y * gravityScale * Time.deltaTime;
            }

            
=======

            yMovement += Physics.gravity.y * gravityScale * Time.deltaTime;
>>>>>>> def7ac5797098317a073a7dd75e017fdd8a6b389

            return yMovement;
        }
    }
}
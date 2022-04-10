using UnityEngine;

namespace Player
{
    public class PlayerMovementController
    {
        public static bool CanJump = true;

        public static Quaternion GetRotation(Quaternion currentRotation)
        {
            if (Mathf.Approximately(InputController.HorizontalAxis, 0f)) return currentRotation;

            var desiredMoveDirection = Camera.main.transform.right * InputController.HorizontalAxis;
            return Quaternion.LookRotation(desiredMoveDirection);
        }

        public static Vector3 VerifyWorldLimits(Vector3 playerPosition)
        {
            if (playerPosition.y > WorldConfig.YMax)
                playerPosition.y = WorldConfig.YMax;

            if (playerPosition.y < WorldConfig.YMin)
                playerPosition.y = WorldConfig.YMin;

            if (playerPosition.x > WorldConfig.XMax)
                playerPosition.x = WorldConfig.XMax;

            if (playerPosition.x < WorldConfig.XMin)
                playerPosition.x = WorldConfig.XMin;

            return playerPosition;
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
            var speed = InputController.IsSprinting ? moveSpeed * 2f : moveSpeed;

            //User input to Movement
            return -InputController.HorizontalAxis * speed;
        }

        private static float GetVerticalMovement(float y, bool isGrounded, float jumpForce, float gravityScale)
        {
            var yMovement = y;

            if (isGrounded)
            {
                yMovement = 0f;
                CanJump = true;
            }

            if (InputController.IsJumping && CanJump)
            {
                yMovement = jumpForce;
                CanJump = false;
            }

            if (!isGrounded)
            {
                yMovement += Physics.gravity.y * gravityScale * Time.deltaTime;
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
    }
}
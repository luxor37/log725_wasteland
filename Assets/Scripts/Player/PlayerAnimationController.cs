using UnityEngine;

namespace Player
{
    public class PlayerAnimationController
    {
        private static bool isJumping = false;
        public static void Animate(Animator animator, bool isGrounded, bool isClimbing)
        {
            bool hasHorizontal = !Mathf.Approximately(InputController.HorizontalAxis, 0.0f);
            animator.SetBool("isRunning", hasHorizontal);

            if (!isGrounded && InputController.IsJumping)
            {
                isJumping = true;
            }

            if (isGrounded)
            {
                if (InputController.IsJumping)
                    isJumping = true;
                isJumping = false;
            }

            animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
            animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
            if (isClimbing)
            {
                if (InputController.VerticalDirection == VerticalDirection.Iddle)
                    animator.speed = 0;
                else
                    animator.speed = 1;
            }
            else
                animator.speed = 1;

            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController
    {
        private static bool _isJumping;
        public static void Animate(Animator animator, bool isGrounded, bool isClimbing)
        {
            var hasHorizontal = !Mathf.Approximately(InputController.HorizontalAxis, 0.0f);
            animator.SetBool("isRunning", hasHorizontal);

            if (!isGrounded && InputController.IsJumping)
                _isJumping = true;

            if (isGrounded)
            {
                if (InputController.IsJumping)
                    _isJumping = true;
                _isJumping = false;
            }

            animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
            animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
            if (isClimbing)
            {
                if (InputController.VerticalDirection == VerticalDirection.Idle)
                    animator.speed = 0;
                else
                    animator.speed = 1;
            }
            else
                animator.speed = 1;

            animator.SetBool("isJumping", _isJumping);
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
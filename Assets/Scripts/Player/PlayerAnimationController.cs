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

            switch (isGrounded)
            {
                case false when InputController.IsJumping:
                    _isJumping = true;
                    break;
                case true:
                {
                    if (InputController.IsJumping)
                        _isJumping = true;
                    _isJumping = false;
                    break;
                }
            }

            animator.SetBool("IsClimbing", isClimbing && InputController.VerticalDirection == VerticalDirection.Up);
            animator.SetBool("IsClimbingDown", isClimbing && InputController.VerticalDirection == VerticalDirection.Down);
            if (isClimbing)
            {
                animator.speed = InputController.VerticalDirection == VerticalDirection.Idle ? 0 : 1;
            }
            else
                animator.speed = 1;

            animator.SetBool("isJumping", _isJumping);
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
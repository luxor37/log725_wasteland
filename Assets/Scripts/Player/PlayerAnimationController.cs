using UnityEngine;

namespace Player
{
    public class PlayerAnimationController
    {
        private static bool isJumping = false;
        public static void Animate(Animator animator, bool isGrounded)
        {
            bool hasHorizontal = !Mathf.Approximately(InputController.HorizontalAxis, 0.0f);
            animator.SetBool("isRunning", hasHorizontal);

            if(!isGrounded && InputController.IsJumping){
                isJumping = true;
            }
            if(isGrounded){
                if(InputController.IsJumping)
                    isJumping = true;
                isJumping = false;
            }

            animator.SetBool("isJumping",isJumping);
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
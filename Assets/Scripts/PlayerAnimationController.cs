using UnityEngine;

public class PlayerAnimationController
{
    public static void Animate(Animator animator, bool isGrounded)
    {
        bool hasHorizontal = !Mathf.Approximately(InputController.HorizontalAxis, 0.0f);
        animator.SetBool("isRunning", hasHorizontal);

        animator.SetBool("isJumping", InputController.IsJumping);
        animator.SetBool("isGrounded", isGrounded);
    }
}
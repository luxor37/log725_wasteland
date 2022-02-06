using UnityEngine;

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

    public static Vector3 GetMovement(float moveSpeed, bool isGrounded, float jumpForce, float gravityScale)
    {
        var desiredMovement = new Vector3(
            GetHorizontalMovement(moveSpeed),
            GetVerticalMovement(isGrounded, jumpForce, gravityScale),
            0f);

        return desiredMovement;
    }

    private static float GetHorizontalMovement(float moveSpeed)
    {
        float speed = InputController.IsSprinting ? moveSpeed * 2f : moveSpeed;

        //User input to Movement
        return -InputController.HorizontalAxis * speed;
    }

    private static float GetVerticalMovement(bool isGrounded, float jumpForce, float gravityScale)
    {
        var grounded = isGrounded;

        var yMovement = 0f;

        if (grounded)
        {
            Debug.Log(InputController.IsJumping);
            if (InputController.IsJumping)
            {
                yMovement = jumpForce;
            }
        }
        else
        {
            yMovement = Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        return yMovement;
    }
}
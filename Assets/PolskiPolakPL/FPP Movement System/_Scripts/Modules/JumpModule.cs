using UnityEngine;

public class JumpModule : MonoBehaviour
{
    [SerializeField] float jumpHeight = 1;

    public void Jump(Motor motor)
    {
        if (!motor.isGrounded)
            return;
        motor.AddVelocity(Vector3.up * GetJumpVelocity());
    }

    float GetJumpVelocity()
    {

        return Mathf.Sqrt(-Physics.gravity.y * jumpHeight * 2);
    }
}

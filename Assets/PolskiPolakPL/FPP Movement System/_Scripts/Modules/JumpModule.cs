using System;
using UnityEngine;

public class JumpModule : MonoBehaviour
{
    [SerializeField] float jumpHeight = 1;
    public event Action OnJump;

    public void Jump(Motor motor)
    {
        if (!motor.isGrounded)
            return;
        motor.AddVelocity(Vector3.up * GetJumpVelocity());
        OnJump?.Invoke();
    }

    float GetJumpVelocity()
    {
        return Mathf.Sqrt(-Physics.gravity.y * jumpHeight * 2);
    }

    private void OnDestroy()
    {
        OnJump = null;
    }
}

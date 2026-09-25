using System;
using UnityEngine;

public class MovementModule : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float accelerationTime = 0.1f;
    [SerializeField] float decelerationTime = 0.1f;
    [SerializeField][Range(0,1)] float airControl = 1f;

    public void ProcessMovement(Vector3 inputDirection, Motor motor)
    {
        Vector3 targetVelocity = GetTargetVelocity(inputDirection);
        Vector3 horizontalVelocity = GetHorizontalVelocity(motor.GetVelocity());

        Vector3 deltaVelocity = GetVelocityChange(horizontalVelocity, targetVelocity);

        if (!motor.isGrounded)
            deltaVelocity *= airControl;

        motor.AddVelocity(deltaVelocity);
    }

    private Vector3 GetVelocityChange(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        return targetVelocity - currentVelocity;
    }

    private Vector3 GetTargetVelocity(Vector3 inputVector)
    {
        return inputVector * speed;
    }

    private Vector3 GetHorizontalVelocity(Vector3 currenVelocity)
    {
        Vector3 horizontalVelocity = new Vector3(currenVelocity.x, 0, currenVelocity.z);
        // ...
        return Vector3.zero;
    }

    Vector3 GetAcceleration(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        return Vector3.zero;
    }

    Vector3 GetDeceleration(Vector3 currentVelocity)
    {
        return Vector3.zero;
    }
}

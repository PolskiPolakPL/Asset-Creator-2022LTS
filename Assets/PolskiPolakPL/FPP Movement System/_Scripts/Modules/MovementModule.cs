using UnityEngine;

public class MovementModule : MonoBehaviour
{
    [SerializeField] float accelerationTime = 0.1f;
    [SerializeField] float decelerationTime = 0.1f;
    [field: SerializeField] public float baseSpeed { get; private set; } = 5;
    [field: SerializeField] public float sprintSpeed { get; private set; } = 8;
    [field: SerializeField] public bool allowSprint { get; private set; } = true;
    [field: SerializeField] public float walkSpeed { get; private set; } = 3;
    [field: SerializeField] public bool allowWalk { get; private set; } = true;
    float targetSpeed;

    public void ProcessMovement(Vector3 inputDirection, Motor motor)
    {
        Vector3 targetVelocity = GetTargetVelocity(inputDirection);
        Vector3 horizontalVelocity = GetHorizontalVelocity(motor.GetVelocity());
        
        Vector3 deltaVelocity = GetVelocityChange(horizontalVelocity, targetVelocity);

        motor.AddVelocity(deltaVelocity);
    }

    public void SetTargetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    private Vector3 GetVelocityChange(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        if(targetVelocity == Vector3.zero)
            return GetDeceleration(currentVelocity);

        return GetAcceleration(currentVelocity, targetVelocity);
    }

    private Vector3 GetTargetVelocity(Vector3 inputVector)
    {
        return inputVector * targetSpeed;
    }

    private Vector3 GetHorizontalVelocity(Vector3 currenVelocity)
    {
        Vector3 horizontalVelocity = new Vector3(currenVelocity.x, 0, currenVelocity.z);
        return horizontalVelocity;
    }

    Vector3 GetAcceleration(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        // calculate acceleration
        float acceleration = baseSpeed / accelerationTime;
        float accDelta = acceleration * Time.deltaTime;

        return Vector3.ClampMagnitude(targetVelocity-currentVelocity, accDelta);
    }

    Vector3 GetDeceleration(Vector3 currentVelocity)
    {
        // calculate deceleration
        float deceleration = baseSpeed / decelerationTime;
        float decDelta = deceleration * Time.deltaTime;
        return Vector3.ClampMagnitude(-currentVelocity, decDelta);
    }
}

using UnityEngine;

public class MovementModule : MonoBehaviour
{
    [SerializeField] float accelerationTime = 0.1f;
    [SerializeField] float decelerationTime = 0.1f;
    [field: SerializeField] public float baseSpeed { get; private set; } = 5;
    [field: Header("Sprinting")]
    [field: SerializeField] public bool enableSprint { get; private set; } = true;
    [field: SerializeField] public float sprintSpeed { get; private set; } = 8;

    [field: Header("Walking")]
    [field: SerializeField] public bool enableWalk { get; private set; } = true;
    [field: SerializeField] public float walkSpeed { get; private set; } = 3;

    // currently aimed speed
    float targetSpeed;

    public void ProcessMovement(Vector3 direction, Motor motor)
    {
        Vector3 targetVelocity = GetTargetVelocity(direction);
        
        Vector3 deltaVelocity = GetVelocityDelta(motor.GetHorizontalVelocity(), targetVelocity);

        motor.AddVelocity(deltaVelocity);
    }

    public void SetTargetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    private Vector3 GetVelocityDelta(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        if(targetVelocity == Vector3.zero)
            return GetDeceleration(currentVelocity);

        return GetAcceleration(currentVelocity, targetVelocity);
    }

    private Vector3 GetTargetVelocity(Vector3 direction)
    {
        return direction * targetSpeed;
    }

    Vector3 GetAcceleration(Vector3 currentVelocity, Vector3 targetVelocity)
    {
        // calculate acceleration
        float acceleration = targetSpeed / accelerationTime;
        float accDelta = acceleration * Time.deltaTime;

        return Vector3.ClampMagnitude(targetVelocity-currentVelocity, accDelta);
    }

    Vector3 GetDeceleration(Vector3 currentVelocity)
    {
        // calculate deceleration
        float deceleration = targetSpeed / decelerationTime;
        float decDelta = deceleration * Time.deltaTime;
        return Vector3.ClampMagnitude(-currentVelocity, decDelta);
    }
}

using UnityEngine;

public class CrouchProneModule : MonoBehaviour
{
    [field: Header("Crouch")]
    [field: SerializeField] public float crouchHeight { get; private set; } = 0.8f;
    [field: SerializeField] public float crouchSpeed { get; private set; } = 2;
    Vector3 crouchCenter;

    [field: Header("Prone")]
    [field: SerializeField] public bool allowProne { get; private set; } = false;
    [field: SerializeField] public float proneHeight { get; private set; } = 0.4f;
    [field: SerializeField] public float proneSpeed { get; private set; } = 1;
    Vector3 proneCenter;

    public PlayerStance currentStance { get; private set; } = PlayerStance.Standing;

    private void Awake()
    {
        crouchCenter = new Vector3(0, crouchHeight / 2, 0);
        proneCenter = new Vector3(0, proneHeight / 2, 0);
    }

    public void Prone(Motor motor)
    {
        motor.SetControllerHeight(proneHeight);
        motor.SetControllerCenter(proneCenter);
        currentStance = PlayerStance.Prone;
    }

    public void Crouch(Motor motor)
    {
        motor.SetControllerHeight(crouchHeight);
        motor.SetControllerCenter(crouchCenter);
        currentStance = PlayerStance.Crouch;
    }

    public void StandUp(Motor motor)
    {
        motor.SetControllerHeight(motor.baseHeight);
        motor.SetControllerCenter(motor.baseCenter);
        currentStance = PlayerStance.Standing;
    }

    public bool CanGetUp(Motor motor, float targetHeight)
    {

        // local to global position
        Vector3 origin = motor.transform.TransformPoint(motor.GetControllerCenter());

        Ray ray = new Ray(origin, Vector3.up);

        //delta H1(target height) - 1/2 H2(current height)
        float range = targetHeight - (motor.GetControllerHeight()/2 + motor.GetControllerRadius());

        return !Physics.SphereCast(ray, motor.GetControllerRadius(), out RaycastHit hit, range);
    }

}


public enum PlayerStance
{
    Standing,
    Crouch,
    Prone
}
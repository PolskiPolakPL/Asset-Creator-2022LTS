using UnityEngine;

public class CrouchModule : MonoBehaviour
{
    [SerializeField] float crouchHeight = 0.8f;
    [field: SerializeField] public float crouchSpeed { get; private set; } = 3;
    Vector3 crouchCenter;

    public float currentHeight { get; private set; }

    public bool isCrouched { get; private set; } = false;

    private void OnValidate()
    {
        crouchCenter = new Vector3(0, crouchHeight/2, 0);
    }

    public void Crouch(Motor motor)
    {
        motor.SetControllerHeight(crouchHeight);
        motor.SetControllerCenter(crouchCenter);
        isCrouched = true;
        currentHeight = crouchHeight;
    }

    public void StandUp(Motor motor)
    {
        motor.SetControllerHeight(motor.baseHeight);
        motor.SetControllerCenter(motor.baseCenter);
        isCrouched = false;
        currentHeight = motor.baseHeight;
    }

    public bool CanStandUp(Motor motor)
    {

        // local to global position
        Vector3 origin = motor.transform.TransformPoint(motor.GetControllerCenter());

        Ray ray = new Ray(origin, Vector3.up);

        //delta H1(base height) - 1/2 H2(crouch height)
        float range = motor.baseHeight - crouchHeight / 2;

        return !Physics.SphereCast(ray, motor.baseRadius, out RaycastHit hit, range);
    }
}

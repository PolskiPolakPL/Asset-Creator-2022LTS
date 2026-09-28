using UnityEngine;

public class CrouchModule : MonoBehaviour
{
    [SerializeField] float crouchHeight = 0.8f;
    [field: SerializeField] public float crouchSpeed { get; private set; } = 3;
    Vector3 crouchCenter;

    float baseHeight;
    Vector3 baseCenter;

    public float currentHeight { get; private set; }

    public bool isCrouched { get; private set; } = false;

    private void OnValidate()
    {
        crouchCenter = new Vector3(0, crouchHeight/2, 0);
    }
    public void Crouch(Motor motor)
    {
        motor.characterController.height = crouchHeight;
        motor.characterController.center = crouchCenter;
        isCrouched = true;
        currentHeight = crouchHeight;
    }

    public void StandUp(Motor motor)
    {
        motor.characterController.height = baseHeight;
        motor.characterController.center = baseCenter;
        isCrouched = false;
        currentHeight = baseHeight;
    }

    public bool TryStandUp(Motor motor)
    {
        CharacterController cc = motor.characterController;

        // local to global position
        Vector3 origin = cc.transform.TransformPoint(cc.center);

        Ray ray = new Ray(origin, Vector3.up);

        //delta H1(base height) - 1/2 H2(crouch height)
        float range = baseHeight - crouchHeight / 2;

        return !Physics.SphereCast(ray, cc.radius, out RaycastHit hit, range);
    }

    public void SetBaseHeight(float height)
    {
        baseHeight = height;
        baseCenter = new Vector3(0, baseHeight/2, 0);
        currentHeight = baseHeight;
    }
}

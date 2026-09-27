using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] KeyBinds actionKey;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;

    Vector3 inputVector;

    // Update is called once per frame
    void Update()
    {
        motor.ApplyGravity();

        //process modules
        ActMovementMod();
        ActJumpModule();

        motor.Move();
    }

    void ActMovementMod()
    {
        if (!movementMod)
            return;
        //move module
        inputVector = InputReader.GetMovementDirection(transform);
        movementMod.ProcessMovement(inputVector, motor);
    }

    void ActJumpModule()
    {
        if (!jumpMod)
            return;
        if (InputReader.Pressed(actionKey.JumpKey))
        {
            jumpMod.Jump(motor);
        }
    }
}

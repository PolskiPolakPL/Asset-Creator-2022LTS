using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] KeyBinds actionKey;
    [SerializeField] InputReader inputReader;
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
        ActMoveMod();
        ActJumpModule();

        motor.Move();
    }

    void ActMoveMod()
    {
        //move module
        inputVector = inputReader.GetMovementDirection();
        movementMod.ProcessMovement(inputVector, motor);
    }

    void ActJumpModule()
    {
        if (jumpMod && inputReader.GetInputKey(actionKey.JumpKey,InputType.PRESS))
        {
            jumpMod.Jump(motor);
        }
    }
}

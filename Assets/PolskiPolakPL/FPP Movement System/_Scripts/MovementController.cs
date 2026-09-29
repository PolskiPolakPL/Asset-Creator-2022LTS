using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] KeyBinds actionKeys;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;
    [SerializeField] CrouchModule crouchMod;

    Vector3 inputVector;

    // Update is called once per frame
    void Update()
    {
        motor.ApplyGravity();

        //process modules
        ActCrouchModule();
        ActJumpModule();
        ActMovementMod();

        motor.Move();
    }

    void ActMovementMod()
    {
        if (!movementMod)
            return;
        //move module
        inputVector = InputReader.GetMovementDirection(playerTransform);
        ProcessSpeed();
        movementMod.ProcessMovement(inputVector, motor);
    }

    void ProcessSpeed()
    {
        if (crouchMod && crouchMod.isCrouched)
        {
            movementMod.SetTargetSpeed(crouchMod.crouchSpeed);
            return;
        }

        if(movementMod.allowWalk && InputReader.Held(actionKeys.WalkKey))
        {
            movementMod.SetTargetSpeed(movementMod.walkSpeed);
            return;
        }

        if (movementMod.allowSprint && InputReader.Held(actionKeys.SprintKey))
        {
            movementMod.SetTargetSpeed(movementMod.sprintSpeed);
            return;
        }

        movementMod.SetTargetSpeed(movementMod.baseSpeed);
    }

    void ActJumpModule()
    {
        if (!jumpMod)
            return;
        if (InputReader.Pressed(actionKeys.JumpKey))
        {
            jumpMod.Jump(motor);
        }
    }

    void ActCrouchModule()
    {
        if (!crouchMod)
            return;

        // Player holds Crouch button
        if (InputReader.Held(actionKeys.CrouchKey))
        {
            if(!crouchMod.isCrouched)
                crouchMod.Crouch(motor);
            return;
        }

        // else if player is standing
        if (!crouchMod.isCrouched)
            return;

        if(crouchMod.CanStandUp(motor))
            crouchMod.StandUp(motor);
    }
}

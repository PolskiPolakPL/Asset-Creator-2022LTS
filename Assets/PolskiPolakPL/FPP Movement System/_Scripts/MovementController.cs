using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] KeyBinds actionKey;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;
    [SerializeField] CrouchModule crouchMod;

    Vector3 inputVector;

    private void Start()
    {
        if(crouchMod)
            crouchMod.SetBaseHeight(motor.characterController.height);
    }

    // Update is called once per frame
    void Update()
    {
        motor.ApplyGravity();

        //process modules
        ActMovementMod();
        ActJumpModule();
        ActCrouchModule();

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

    void ActCrouchModule()
    {
        if (!crouchMod)
            return;

        // Player holds Crouch button
        if (InputReader.Held(actionKey.CrouchKey))
        {
            if(!crouchMod.isCrouched)
                crouchMod.Crouch(motor);
            return;
        }

        // else if player is standing
        if (!crouchMod.isCrouched)
            return;

        if(crouchMod.TryStandUp(motor))
            crouchMod.StandUp(motor);
    }
}

using UnityEngine;

public class FPPMovementController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] KeyBinds actionKeys;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;
    [SerializeField] CrouchProneModule crouchProneMod;

    private void Awake()
    {
        movementMod = GetComponentInChildren<MovementModule>();
        jumpMod = GetComponentInChildren<JumpModule>();
        crouchProneMod = GetComponentInChildren<CrouchProneModule>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.ApplyGravity();

        //process modules
        if(jumpMod)
            ActJumpModule();

        if(crouchProneMod)
            ActCrouchProneMod();

        if(movementMod)
            ActMovementMod();

        motor.Move();
    }

    void ActMovementMod()
    {
        Vector3 inputDirection = InputReader.GetMovementDirection(playerTransform);
        ProcessMovement(inputDirection);
    }

    void ProcessMovement(Vector3 direction)
    {

        if (crouchProneMod)
        {
            // Player CROUCH
            if(crouchProneMod.currentStance == PlayerStance.Crouch)
            {
                movementMod.Move(direction, motor, crouchProneMod.crouchSpeed);
                return;
            }

            // Player PRONE
            if (crouchProneMod.currentStance == PlayerStance.Prone)
            {
                movementMod.Move(direction, motor, crouchProneMod.proneSpeed);
                return;
            }
        }

        // Player WALK
        if(movementMod.enableWalk && InputReader.Held(actionKeys.WalkKey))
        {
            movementMod.Walk(direction, motor);
            return;
        }

        // Player SPRINT
        if (movementMod.enableSprint && InputReader.Held(actionKeys.SprintKey))
        {
            movementMod.Sprint(direction, motor);
            return;
        }

        // Player RUN (default)
        movementMod.Run(direction, motor);
    }

    void ActJumpModule()
    {
        if (InputReader.Pressed(actionKeys.JumpKey))
        {
            jumpMod.Jump(motor);
        }
    }

    void ActCrouchAlone()
    {

        // Player holds Crouch button
        if (InputReader.Held(actionKeys.CrouchKey))
        {
            if (crouchProneMod.currentStance == PlayerStance.Standing)
                crouchProneMod.Crouch(motor);
            return;
        }

        if (crouchProneMod.currentStance == PlayerStance.Standing)
            return;

        if (crouchProneMod.CanGetUp(motor,motor.baseHeight))
            crouchProneMod.StandUp(motor);
    }


    void ActCrouchProneMod()
    {

        if (!crouchProneMod.allowProne)
        {
            ActCrouchAlone();
            return;
        }

        ActCrouchProneToggle();
    }

    void ActCrouchProneToggle()
    {
        if (InputReader.Pressed(actionKeys.CrouchKey))
        {
            //player crouching
            if (crouchProneMod.currentStance.Equals(PlayerStance.Crouch))
            {
                if (crouchProneMod.CanGetUp(motor, motor.baseHeight))
                    crouchProneMod.StandUp(motor);
                return;
            }

            //player is prone
            if (crouchProneMod.currentStance.Equals(PlayerStance.Prone))
            {
                if (crouchProneMod.CanGetUp(motor, crouchProneMod.crouchHeight))
                    crouchProneMod.Crouch(motor);
                return;
            }

            // player standing
            crouchProneMod.Crouch(motor);
        }

        if (InputReader.Pressed(actionKeys.ProneKey))
        {
            //player is NOT prone
            if (crouchProneMod.currentStance != PlayerStance.Prone)
            {
                crouchProneMod.Prone(motor);
                return;
            }

            // player IS prone
            if (crouchProneMod.CanGetUp(motor, motor.baseHeight))
                crouchProneMod.StandUp(motor);
        }

        if (InputReader.Pressed(actionKeys.SprintKey))
        {
            if (crouchProneMod.currentStance == PlayerStance.Standing)
                return;
            if (crouchProneMod.CanGetUp(motor, motor.baseHeight))
                crouchProneMod.StandUp(motor);
        }
    }
}

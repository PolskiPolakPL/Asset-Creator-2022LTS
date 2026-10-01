using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] KeyBinds actionKeys;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;
    [SerializeField] CrouchProneModule crouchProneMod;

    [Header("VFX and SFX")]
    [SerializeField] MovementAudio audioController;

    Vector3 inputVector;

    private void Awake()
    {
        if (audioController)
            SubscribeAudio();
    }

    void SubscribeAudio()
    {
        motor.OnLanded += audioController.PlayRandomLandingSound;

        if (jumpMod)
            jumpMod.OnJump += audioController.PlayRandomJumpSound;

        if (crouchProneMod)
            crouchProneMod.OnStanceChanged += audioController.PlayStanceChangeSound;
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
        inputVector = InputReader.GetMovementDirection(playerTransform);
        ProcessSpeed();
        movementMod.ProcessMovement(inputVector, motor);
    }

    void ProcessSpeed()
    {

        if (crouchProneMod)
        {
            if(crouchProneMod.currentStance == PlayerStance.Crouch)
            {
                movementMod.SetTargetSpeed(crouchProneMod.crouchSpeed);
                return;
            }

            if (crouchProneMod.currentStance == PlayerStance.Prone)
            {
                movementMod.SetTargetSpeed(crouchProneMod.proneSpeed);
                return;
            }
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
            if (crouchProneMod.CanGetUp(motor, motor.baseHeight))
                crouchProneMod.StandUp(motor);
        }
    }
}

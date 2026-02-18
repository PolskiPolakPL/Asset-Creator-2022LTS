using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSMovement : MonoBehaviour
{
    private CharacterController controller;


    [Header("Movement")]
    [SerializeField] float baseSpeed = 2.5f;


    [Header("Sprint")]
    public bool CanSprint = true;
    [SerializeField] KeyCode runningKey = KeyCode.LeftShift;
    [SerializeField] float runningSpeed = 5;


    [Header("Stamina")]
    [SerializeField] PlayerVitals playerVitals;
    [SerializeField] float drainSpeed = 5;
    [SerializeField] float regenSpeed = 3;

    [Header("Jump")]
    public bool CanJump = true;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] float jumpHeight = 1.0f;

    private Vector3 playerVelocity;
    private float gravityValue = Physics.gravity.y;
    private bool groundedPlayer;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerVitals.Stamina.OnDrained += DisableRunning;
        playerVitals.Stamina.OnFilled += EnableRunning;
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y <= 0)
        {
            playerVelocity.y = 0f;
        }

        PlayerMove();

        if (Input.GetKey(jumpKey))
            PlayerJump();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (CheckSprinting())
            DrainStamina();
        else
            RegenStamina();
        float speed = SetMovementSpeed();

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(speed * Time.deltaTime * move);
    }

    void PlayerJump()
    {
        if (!groundedPlayer || !CanJump)
            return;
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    float SetMovementSpeed()
    {
        if (CheckSprinting())
            return runningSpeed;
        else
            return baseSpeed;
    }

    void DrainStamina()
    {
        playerVitals.Stamina.Loose(drainSpeed * Time.deltaTime);
    }

    void RegenStamina()
    {
        playerVitals.Stamina.Gain(regenSpeed * Time.deltaTime);
    }

    bool CheckSprinting()
    {//Is moving (X&Z Axis) AND holds running key
        return (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0) && Input.GetKey(runningKey) && CanSprint;
    }

    void DisableRunning()
    {
        CanSprint = false;
    }

    void EnableRunning()
    {
        CanSprint = true;
    }
}

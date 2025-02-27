using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;


    [Header("PlayerScript Movement")]
    [SerializeField] float playerSpeed = 3.5f;

    [Header("PlayerScript Jump")]
    public bool CanJump = true;
    [SerializeField] float jumpHeight = 1.0f;
    private float gravityValue = Physics.gravity.y;
    private Vector3 playerVelocity;
    private bool groundedPlayer;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y <= 0)
        {
            playerVelocity.y = 0f;
        }

        PlayerMove();
        if (Input.GetButtonDown("Jump") && groundedPlayer && CanJump)
            PlayerJump();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(playerSpeed * Time.deltaTime * move);
    }

    void PlayerJump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}

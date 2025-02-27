using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] Camera playerCamera;
    [SerializeField] float mouseSensitivity = 1f;
    private float xRotation = 0f;

    void Awake()
    {
        if(!playerCamera)
            playerCamera = Camera.main;
    }

    void Update()
    {
        Look();
    }

    void Look()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player's body left and right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up and down (clamping to 90 degrees)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

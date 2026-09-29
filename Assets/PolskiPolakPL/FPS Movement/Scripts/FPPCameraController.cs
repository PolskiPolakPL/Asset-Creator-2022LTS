using UnityEngine;

public class FPPCameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Camera playerCamera;
    [SerializeField] float mouseSensitivity = 1f;
    public bool invertYAxis = false;
    public bool invertXAxis = false;
    private float xRotation = 0f;

    Vector3 baseCamOffset;

    [Header("Modules")]
    [SerializeField] CrouchModule crouchMod;
    [SerializeField] float crouchCamHeight = 1f;
    [SerializeField] CrouchProneModule crouchProneMod;
    [SerializeField] float proneCamHeight = 1f;
    void Awake()
    {
        if(!playerCamera)
            playerCamera = Camera.main;
        baseCamOffset = playerCamera.transform.localPosition;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
        UpdateCameraHeight();
    }

    void UpdateCameraHeight()
    {
        if (!crouchMod && !crouchProneMod)
            return;

        if (crouchMod && crouchMod.isCrouched)
        {
            SetCameraHeight(crouchCamHeight);
            return;
        }

        if (crouchProneMod)
        {
            if(crouchProneMod.currentStance == PlayerStance.Crouch)
            {
                SetCameraHeight(crouchCamHeight);
                return;
            }

            if(crouchProneMod.currentStance == PlayerStance.Prone)
            {
                SetCameraHeight(proneCamHeight);
                return;
            }
        }

        playerCamera.transform.localPosition = baseCamOffset;
    }

    void SetCameraHeight(float height)
    {
        playerCamera.transform.localPosition = new Vector3(baseCamOffset.x, height, baseCamOffset.z);
    }

    void Look()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        if(invertXAxis)
            mouseX = -mouseX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        if (invertYAxis)
            mouseY = -mouseY;

        // Rotate the player's body left and right
        playerTransform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up and down (clamping to 90 degrees)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

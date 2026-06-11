using UnityEngine;

public class RTSCameraOrbit : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform rig;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float initialPitch = 45f;
    [SerializeField] private float minPitch = 10f;
    [SerializeField] private float maxPitch = 89f;

    [Header("Zoom")]
    [SerializeField] private float zoomDistance = 20f;
    [SerializeField] private float minZoomDistance = 5f;
    [SerializeField] private float maxZoomDistance = 40f;
    [SerializeField] private float zoomSpeed = 5f;

    private float pitch;

    private void Awake()
    {
        pitch = initialPitch;

        if (rig == null)
        {
            Debug.LogError($"{this.name} requires a Rig reference.");
        }
    }

    private void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    private void LateUpdate()
    {
        UpdateCameraTransform();
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!Input.GetMouseButton(2))
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rig.Rotate(Vector3.up, mouseX * rotationSpeed);

        pitch = Mathf.Clamp(pitch + mouseY * rotationSpeed, minPitch,  maxPitch);
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Approximately(scroll, 0f))
            return;

        zoomDistance = Mathf.Clamp(zoomDistance - scroll * zoomSpeed, minZoomDistance, maxZoomDistance);
    }

    private void UpdateCameraTransform()
    {
        float pitchRad = pitch * Mathf.Deg2Rad;

        float horizontalDistance = zoomDistance * Mathf.Cos(pitchRad);

        float height = zoomDistance * Mathf.Sin(pitchRad);

        transform.position = rig.position - rig.forward * horizontalDistance + Vector3.up * height;
        transform.LookAt(rig.position);
    }
}
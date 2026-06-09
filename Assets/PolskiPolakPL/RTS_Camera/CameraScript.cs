using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float polarAngle = 45f;
    [SerializeField] float radialDistance = 20f;

    [Header("Camera Movement")]
    [SerializeField] float baseSpeed = 10;
    [SerializeField] float fastSpeed = 20;
    [SerializeField] float rotationSpeed = 2;
    [Header("Camera Zoom")]
    [SerializeField] float minZoomDistance = 3;
    [SerializeField] float maxZoomDistance = 20;
    [SerializeField] float zoomAmount = 5;
    [Header("Camera Border")]
    [SerializeField] bool isBorderActive = false;
    [SerializeField] int margin = 1;

    Vector3 newPosition;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        startPosition = transform.position;
        if(cam==null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        HandleRigMovement();

        HandleCameraRotation();
        cam.transform.position = getNewCameraPosition();
        cam.transform.LookAt(transform.position);

        // changing distance (zoom)
        if (Input.mouseScrollDelta.y != 0)
        {
            radialDistance = Mathf.Clamp(radialDistance - Input.mouseScrollDelta.y * zoomAmount, minZoomDistance, maxZoomDistance);
        }

        //porót do pozycji startowej
        if (Input.GetKeyDown(KeyCode.C))
        {
            newPosition = startPosition;
        }
    }

    private Vector3 getNewCameraPosition()
    {
        float h = radialDistance * Mathf.Sin(polarAngle * Mathf.Deg2Rad);
        float x = Mathf.Sqrt(Mathf.Pow(radialDistance, 2) - Mathf.Pow(h, 2));

        return transform.position - transform.forward * x + Vector3.up * h;
    }

    void HandleCameraRotation()
    {
        // rotating camera
        if (Input.GetMouseButtonDown(2))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButton(2)) // MMB
        {
            // horizontally
            if (Input.GetAxis("Mouse X") != 0)
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
            }

            // vertically
            if (Input.GetAxis("Mouse Y") != 0)
            {
                polarAngle = Mathf.Clamp(polarAngle + Input.GetAxis("Mouse Y") * rotationSpeed, 0, 89.9f);
            }
        }

        if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void HandleRigMovement()
    {
        Vector3 mousePosition = Input.mousePosition;
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? fastSpeed : baseSpeed;
        speed *= Time.deltaTime;

            // up edge detected
        if (IsCursorUp(mousePosition) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * speed);
        }
            // down edge detected
        if (IsCursorDown(mousePosition) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -speed);
        }
            // left edge detected
        if (IsCursorLeft(mousePosition) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -speed);
        }
            // right edge detected
        if (IsCursorRight(mousePosition) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * speed);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);
    }

    bool IsCursorUp(Vector3 mousePosition)
    {
        return isBorderActive && mousePosition.y >= Screen.height - margin;
    }

    bool IsCursorDown(Vector3 mousePosition)
    {
        return isBorderActive && mousePosition.y <= margin;
    }

    bool IsCursorLeft(Vector3 mousePosition)
    {
        return isBorderActive && mousePosition.x <= margin;
    }

    bool IsCursorRight(Vector3 mousePosition)
    {
        return isBorderActive && mousePosition.x >= Screen.width - margin;
    }
}

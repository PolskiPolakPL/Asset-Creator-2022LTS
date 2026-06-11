using UnityEngine;

public class RTSCameraRig : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float fastSpeed = 20f;
    [SerializeField] private float smoothTime = 10f;

    [Header("Border Scrolling")]
    [SerializeField] private bool borderScrolling = false;
    [SerializeField] private int margin = 5;

    private Vector3 targetPosition;
    private Vector3 initialPosition;

    private void Awake()
    {
        targetPosition = transform.position;
        initialPosition = transform.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleReset();

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
    }

    private void HandleMovement()
    {
        Vector2 input = GetMovementInput();

        if (input.sqrMagnitude <= 0f)
            return;

        float speed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : baseSpeed;

        Vector3 movement = transform.forward * input.y + transform.right * input.x;

        movement.y = 0f;

        targetPosition += movement.normalized * speed * Time.deltaTime;
    }

    private Vector2 GetMovementInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            input.y += 1;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            input.y -= 1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            input.x += 1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            input.x -= 1;

        if (borderScrolling)
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition.y >= Screen.height - margin)
                input.y += 1;

            if (mousePosition.y <= margin)
                input.y -= 1;

            if (mousePosition.x >= Screen.width - margin)
                input.x += 1;

            if (mousePosition.x <= margin)
                input.x -= 1;
        }

        return input;
    }

    private void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            targetPosition = initialPosition;
        }
    }
}
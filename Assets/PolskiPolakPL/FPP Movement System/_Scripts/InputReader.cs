using UnityEngine;

public class InputReader : MonoBehaviour
{
    Vector3 inputVector;

    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    public Vector3 GetMovementDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        inputVector = transform.right * moveX + transform.forward * moveZ;

        return inputVector.normalized;
    }

    public bool GetJumpKeyDown()
    {
        return Input.GetKeyDown(jumpKey);
    }

    public bool GetInputKey(KeyCode keyCode, InputType inputType)
    {
        switch (inputType)
        {
            case InputType.PRESS:
                return Input.GetKeyDown(keyCode);

            case InputType.RELEASE:
                return Input.GetKeyUp(keyCode);

            case InputType.HOLD:
                return Input.GetKey(keyCode);

            case InputType.DOUBLE_TAP:
                return GetDoubleKeyDown(keyCode);

            default:
                return false;
        }
    }
    bool GetDoubleKeyDown(KeyCode keyCode)
    {
        return false;
    }
}




public enum InputType
{
    PRESS,
    RELEASE,
    HOLD,
    DOUBLE_TAP
}
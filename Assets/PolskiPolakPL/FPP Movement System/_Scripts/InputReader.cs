using UnityEngine;

public static class InputReader
{
    static Vector3 inputVector;

    public static Vector3 GetMovementDirection(Transform targetT)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        inputVector = targetT.right * moveX + targetT.forward * moveZ;

        return inputVector.normalized;
    }

    public static bool Pressed(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public static bool Released(KeyCode keyCode)
    {
        return Input.GetKeyUp(keyCode);
    }

    public static bool Held(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public static bool DoubleTapped(KeyCode keyCode)
    {
        return false;
    }
}
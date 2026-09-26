using UnityEngine;

public class InputReader : MonoBehaviour
{
    Vector3 inputVector;

    public Vector3 GetMovementDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        inputVector = transform.right * moveX + transform.forward * moveZ;

        return inputVector.normalized;
    }
}

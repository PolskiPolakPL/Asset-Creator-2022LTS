using UnityEngine;

public class InputReader : MonoBehaviour
{

    public Vector3 GetMovementDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        return new Vector3(moveX, 0, moveZ).normalized;
    }
}

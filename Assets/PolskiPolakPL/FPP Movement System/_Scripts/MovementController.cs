using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] InputReader inputReader;
    [SerializeField] Motor motor;

    [Header("Movement Modules")]
    [SerializeField] MovementModule movementMod;

    Vector3 inputVector;

    // Update is called once per frame
    void Update()
    {
        motor.UpdateMotor();

        inputVector = inputReader.GetMovementDirection();
        movementMod.ProcessMovement(inputVector, motor);

        motor.Move();
    }
}

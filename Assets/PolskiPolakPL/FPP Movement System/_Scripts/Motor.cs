using UnityEngine;
public class Motor : MonoBehaviour
{
    [field: SerializeField] public CharacterController characterController {  get; private set; }
    Vector3 velocity;
    [field: SerializeField] public bool isGrounded {  get; private set; }

    public void UpdateMotor()
    {
        //Tymczasowo puste
    }

    public void ApplyGravity()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded)
            velocity.y = 0;
        velocity += Physics.gravity * Time.deltaTime;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void AddVelocity(Vector3 velocity)
    {
        this.velocity += velocity;
    }

    public void Move()
    {
        characterController.Move(velocity * Time.deltaTime);
    }
}

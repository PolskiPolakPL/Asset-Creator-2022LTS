using UnityEngine;
public class Motor : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    Vector3 velocity;
    public bool isGrounded {  get; private set; }

    public void UpdateMotor()
    {
        //apply gravity and starting velocity
        velocity += Physics.gravity * Time.deltaTime;

        //check grounded
        isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            velocity.y = Mathf.Max(velocity.y, 0);
        }
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

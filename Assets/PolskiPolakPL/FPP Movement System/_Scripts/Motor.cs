using System;
using UnityEngine;
public class Motor : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    Vector3 velocity;
    [field: SerializeField] public bool isGrounded { get; private set; }

    public float baseHeight { get; private set; }
    public float baseRadius { get; private set; }
    public Vector3 baseCenter { get; private set; }

    public event Action OnLanded;

    private void Awake()
    {
        SetBaseValues();
    }
    void SetBaseValues()
    {
        baseHeight = characterController.height;
        baseRadius = characterController.radius;
        baseCenter = characterController.center;
    }
    public void ApplyGravity()
    {
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

    #region CahracterController Collider GETs & SETs
    public float GetControllerHeight()
    {
        return characterController.height;
    }
    public void SetControllerHeight(float height)
    {
        characterController.height = height;
    }

    public float GetControllerRadius()
    {
        return characterController.radius;
    }
    public void SetControllerRadius(float radius)
    {
        characterController.radius = radius;
    }

    public Vector3 GetControllerCenter()
    {
        return characterController.center;
    }
    public void SetControllerCenter(Vector3 center)
    {
        characterController.center = center;
    }
    #endregion

    public void Move()
    {
        characterController.Move(velocity * Time.deltaTime);

        if (isGrounded != characterController.isGrounded)
        {
            if (!isGrounded)
                OnLanded?.Invoke();
            isGrounded = characterController.isGrounded;
        }
    }

    private void OnDestroy()
    {
        OnLanded = null;
    }
}

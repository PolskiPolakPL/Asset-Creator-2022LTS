using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [Range(0.1f, 1)][SerializeField] float rotationDelay = 0.5f;
    float rotationSpeed;

    private void OnValidate()
    {
        rotationSpeed = (1 / rotationDelay);
    }
    private void LateUpdate()
    {
        Observe(target.position + targetOffset, rotationSpeed);
    }

    public void Observe(Vector3 targetPosition, float rotationSpeed)
    {
        Vector3 direction = targetPosition - transform.position;
        if (direction == Vector3.zero)
            return;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationSpeed * Time.deltaTime);
    }

}

using UnityEngine;

namespace PolskiPolakPL.Utils
{
    public class TransformPlus
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformPlus(Transform transform)
        {
            this.position = transform.position;
            this.rotation = transform.rotation;
        }
        public Quaternion SmoothRotation(Vector3 targetPosition, float rotationSpeed)
        {
            Vector3 direction = targetPosition - position;
            if (direction == Vector3.zero)
                return rotation;
            Quaternion startRotation = rotation;
            Quaternion endRotation = Quaternion.LookRotation(direction);
            return rotation = Quaternion.Slerp(startRotation, endRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

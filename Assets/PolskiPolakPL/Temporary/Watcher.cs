using UnityEngine;
using PolskiPolakPL.Utils;

public class Watcher : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [Range(0.1f, 1)][SerializeField] float rotationDelay = 0.5f;
    float rotationSpeed;

    Timer timer;
    TransformPlus newTransform;
    private void Awake()
    {
        newTransform = new TransformPlus(transform);
    }
    private void OnValidate()
    {
        rotationSpeed = (1 / rotationDelay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = newTransform.SmoothRotation(target.position+targetOffset, rotationSpeed);
    }

}

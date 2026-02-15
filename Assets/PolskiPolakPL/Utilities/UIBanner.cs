using UnityEngine;

public class UIBanner : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0,1,0);
    [SerializeField] Transform target;
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }
}

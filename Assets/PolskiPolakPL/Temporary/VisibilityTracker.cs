using TMPro;
using UnityEngine;

public class VisibilityTracker : MonoBehaviour
{
    [SerializeField] TMP_Text DebugTextField;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask occluderLayer;

    Collider collider;
    Plane[] cameraFrustumPlanes;
    // Start is called before the first frame update
    void Start()
    {
        if(!playerCamera)
            playerCamera = Camera.main;
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible())
            SightDebugMessage("<color=#00FF00> Target On Sight!</color>");
        else
            SightDebugMessage("<color=#FF0000> Target Off Sight!</color>");

        if (IsOccluded())
            Debug.DrawLine(playerCamera.transform.position, transform.position, Color.red);
        else
            Debug.DrawLine(playerCamera.transform.position, transform.position, Color.green);
    }

    bool IsVisible()
    {
        Bounds bounds = collider.bounds;
        cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        return GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, bounds);
    }

    bool IsOccluded()
    {
        Debug.DrawLine(playerCamera.transform.position, transform.position, Color.green);
        return Physics.Linecast(playerCamera.transform.position, transform.position, occluderLayer);
    }

    void SightDebugMessage(string message)
    {
        if (DebugTextField)
            DebugTextField.text = message;
        else Debug.Log(message);
    }
}

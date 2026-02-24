using TMPro;
using UnityEngine;
using PolskiPolakPL.Utils;
using System;
public class Visibility : MonoBehaviour
{
    [SerializeField] TMP_Text DebugTextField;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask occluderLayer;
    public bool isVisible { get; private set; } = true;

    public event Action OnBecameVisible;
    public event Action OnBecameHidden;

    Bounds bounds;
    Collider collider;
    Plane[] cameraFrustumPlanes;


    private void Awake()
    {
        OnBecameHidden += ReportHidden;
        OnBecameVisible += ReportVisible;
    }

    void Start()
    {
        if(!playerCamera)
            playerCamera = Camera.main;
        collider = gameObject.GetComponent<Collider>();
        bounds = collider.bounds;
        SetIsVisible(CheckInCameraView() && !CheckFullyOccluded());
        if(CheckInCameraView() && !CheckFullyOccluded())
            ReportVisible();
        else
            ReportHidden();
    }

    // Update is called once per frame
    void Update()
    {
        bounds = collider.bounds;
        Debugging.DrawBounds(bounds, Color.white);
        if (!CheckInCameraView())
        {
            SetIsVisible(false);
            return;
        }
        if(CheckFullyOccluded())
            SetIsVisible(false);
        else
            SetIsVisible(true);
    }

    bool CheckInCameraView()
    {
        cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        return GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, bounds);
    }

    bool CheckFullyOccluded()
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        Vector3[] points =
        {
            // Bottom
            new Vector3(min.x, min.y, min.z),
            new Vector3(max.x, min.y, min.z),
            new Vector3(max.x, min.y, max.z),
            new Vector3(min.x, min.y, max.z),
            // Top
            new Vector3(min.x, max.y, min.z),
            new Vector3(max.x, max.y, min.z),
            new Vector3(max.x, max.y, max.z),
            new Vector3(min.x, max.y, max.z),
            bounds.center
        };

        Vector3 originPoint = playerCamera.transform.position;
        Vector3 direction;
        float distance;
        foreach(Vector3 targetPoint in points)
        {
            direction = (targetPoint - originPoint).normalized;
            distance = Vector3.Distance(targetPoint, originPoint);
            if (!Physics.Raycast(originPoint, direction, out RaycastHit hit, distance, occluderLayer) || hit.transform == transform)
                return false;
        }
        return true;
    }

    void ReportVisible()
    {
        SightDebugMessage($"<color=#00FF00> {gameObject.name} is on Sight!</color>");
    }

    void ReportHidden()
    {
        SightDebugMessage($"<color=#FF0000> {gameObject.name} is off Sight!</color>");
    }

    void SightDebugMessage(string message)
    {
        if (DebugTextField)
            DebugTextField.text = message;
        else Debug.Log(message);
    }

    void SetIsVisible(bool isVisible)
    {
        if (this.isVisible == isVisible)
            return;

        this.isVisible = isVisible;

        if (isVisible)
        {
            OnBecameVisible?.Invoke();
        }
        else
        {
            OnBecameHidden?.Invoke();
        }
    }
}

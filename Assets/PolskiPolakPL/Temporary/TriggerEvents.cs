using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvents : MonoBehaviour
{
    [Header("Detection Filters")] // - If a Collider is detected by at least one of the filters, the events will be invoked.
    [Tooltip("List of Layers that activate trigger.")]
    [SerializeField] LayerMask includeLayers;
    [Tooltip("List of Tags that activate trigger.")]
    [SerializeField] List<string> includeTags;

    [Header("Trigger Events")]
    public UnityEvent<Collider> OnEnter;
    public UnityEvent<Collider> OnStay;
    public UnityEvent<Collider> OnExit;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger=true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DetectLayer(other) || DetectTag(other))
            OnEnter?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (DetectLayer(other) || DetectTag(other))
            OnStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (DetectLayer(other) || DetectTag(other))
            OnExit?.Invoke(other);
    }

    private bool DetectTag(Collider other)
    {
        foreach (string tag in includeTags)
        {
            if (string.IsNullOrEmpty(tag))
            {
                Debug.LogWarning($"[{this}]: Tag cannot be NULL or Empty!");
                return false;
            }
            if(other.CompareTag(tag))
                return true;
        }
        return false;
    }

    private bool DetectLayer(Collider other)
    {
        if((includeLayers & (1<<other.gameObject.layer))!=0)
            return true;
        return false;
    }
}

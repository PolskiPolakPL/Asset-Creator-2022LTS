using System;
using UnityEngine;

public class PickableScript : MonoBehaviour, IPickable
{
    Interactable interactable;
    public ItemData itemSO;
    public event Action<ItemData> OnPickUp;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += PickUp;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"You've picked up '{gameObject.name}'!");
        interactable.OnInteraction -= PickUp;
    }
}

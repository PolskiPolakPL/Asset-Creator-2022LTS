using System;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickable
{
    Interactable interactable;
    public ItemData itemSO;
    public event Action OnPickUp;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += PickUp;
    }

    public void PickUp()
    {
        if (!InventorySystem.Instance.AddItem(itemSO))
            return;
        OnPickUp?.Invoke();
        Debug.Log($"You've picked up '{gameObject.name}'!");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= PickUp;
    }
}

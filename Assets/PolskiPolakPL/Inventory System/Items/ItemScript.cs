using System;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickable
{
    Interactable interactable;
    public event Action OnPickUp;


    public ItemData itemSO;
    [Min(1)] public int amount = 1;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += PickUp;
    }

    public void PickUp()
    {
        if (!InventorySystem.Instance.AddItem(itemSO, amount))
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

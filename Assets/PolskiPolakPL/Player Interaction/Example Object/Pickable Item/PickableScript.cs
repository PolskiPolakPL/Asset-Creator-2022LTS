using System;
using UnityEngine;

public class PickableScript : MonoBehaviour
{
    Interactable interactable;
    public Item itemSO;
    public event Action<Item> OnPickUp;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += PickUp;
    }

    void PickUp()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"You've picked up '{gameObject.name}'!");
        interactable.OnInteraction -= PickUp;
    }
}

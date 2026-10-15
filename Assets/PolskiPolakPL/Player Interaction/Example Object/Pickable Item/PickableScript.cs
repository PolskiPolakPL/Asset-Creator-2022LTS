using System;
using UnityEngine;

public class PickableScript : MonoBehaviour
{
    InteractableObject interactable;
    public Item itemSO;
    public event Action<Item> OnPickUp;
    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteraction.AddListener(PickUp);
    }

    void PickUp()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"You've picked up '{gameObject.name}'!");
        interactable.OnInteraction.RemoveListener(PickUp);
    }
}

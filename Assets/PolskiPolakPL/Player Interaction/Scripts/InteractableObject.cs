using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{

    public string message1;
    public UnityEvent OnInteraction;

    public string message2;
    public UnityEvent OnAltInteraction;

    public event Action OnPointerEnter;
    public event Action OnPointerExit;

    public void Interact()
    {
        OnInteraction?.Invoke();
    }

    public void AltInteract()
    {
        OnAltInteraction?.Invoke();
    }

    public void Select()
    {
        OnPointerEnter?.Invoke();
    }
    public void Deselect()
    {
        OnPointerExit?.Invoke();
    }
}

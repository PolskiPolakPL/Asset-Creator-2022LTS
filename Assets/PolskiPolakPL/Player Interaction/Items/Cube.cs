using UnityEngine;

public class DisplayCubeMessage : MonoBehaviour
{
    Interactable interactable;
    [SerializeField] string message;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += DisplayMessage;
    }

    void DisplayMessage()
    {
        Debug.Log($"{gameObject.name}: '{message}'");
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= DisplayMessage;
    }
}

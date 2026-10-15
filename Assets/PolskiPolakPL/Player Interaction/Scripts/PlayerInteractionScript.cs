using UnityEngine;

/// <summary>
/// Interaction System made with this
/// <seealso href="https://youtu.be/b7Yf6BFx6js">tutorial</seealso>
/// </summary>
public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] Transform cameraT;
    [SerializeField] float playerReach = 2;
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode altInteractionKey = KeyCode.F;

    InteractableObject currentInteractable;
    InteractableObject newInteractable;
    RaycastHit hit;
    Ray ray;
    public Transform interactiontHitT {  get; private set; }

    private void Start()
    {
        if (!cameraT)
            cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (!currentInteractable)
            return;

        if (Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
            return;
        }

        if (Input.GetKeyDown(altInteractionKey))
        {
            currentInteractable.AltInteract();
        }
    }

    void CheckInteraction()
    {
        ray = new Ray(cameraT.position, cameraT.forward);
        Debug.DrawRay(cameraT.position, cameraT.forward * playerReach,Color.blue);
        if (!Physics.Raycast(ray, out hit, playerReach) || !(hit.collider.TryGetComponent<InteractableObject>(out newInteractable)))
        {
            DisableCurrentInteractable();
            return;
        }
        if(currentInteractable && newInteractable != currentInteractable)
        {
            DisableCurrentInteractable();
        }
        if (newInteractable.enabled)
        {
            SetNewCurrentInteractable(newInteractable);
        }
        else
        {
            DisableCurrentInteractable();
        }

    }

    void SetNewCurrentInteractable(InteractableObject newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.Select();

        if (!InteractionUIManager.Instance)
            return;

        DisplayInteractionMessage();
    }

    void DisableCurrentInteractable()
    {
        if (InteractionUIManager.Instance)
            InteractionUIManager.Instance.HideInteractionText();
        if (!currentInteractable)
            return;
        currentInteractable.Deselect();
        currentInteractable = null;
    }

    void DisplayInteractionMessage()
    {
        string message = "";

        if (currentInteractable.message1 != "")
            message += $"[{interactionKey}] {currentInteractable.message1}";

        if(currentInteractable.message2 != "")
            message += $"\n[{altInteractionKey}] {currentInteractable.message2}";

        InteractionUIManager.Instance.DisplayInteractionText(message);
    }
}


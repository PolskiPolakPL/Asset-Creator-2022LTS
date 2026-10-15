using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    Animator doorAnimator;
    [SerializeField] bool isOpened = false;
    [SerializeField] bool isLocked = false;
    [SerializeField] bool allowUnlock = true;

    InteractableObject interactable;
    Collider doorCollider;

    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        doorAnimator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();

        interactable.OnInteraction.AddListener(DoInteraction);
        interactable.OnAltInteraction.AddListener(DoAltInteraction);

        if(isOpened)
            interactable.message1 = "Close";
        else
            interactable.message1 = "Open";

        if (isLocked)
            interactable.message2 = "Unlock";
        else
            interactable.message2 = "Lock";
    }

    public void DoInteraction()
    {
        if (isOpened)
            Close();
        else if(!isLocked)
            Open();
    }

    public void DoAltInteraction()
    {
        if(isLocked)
            TryUnlock(allowUnlock);
        else
            TryLock();
    }

    void TryLock()
    {
        if (isOpened)
            return;
        isLocked = true;
        interactable.message2 = "Unlock";
    }

    void TryUnlock(bool canUnlock)
    {
        if (!canUnlock)
            return;
        isLocked = false;
        interactable.message2 = "Lock";
    }

    void Open()
    {
        doorAnimator.Play("OpenDoorAnimation");
        isOpened = true;
        interactable.message1 = "Close";
    }

    void Close()
    {
        doorAnimator.Play("CloseDoorAnimation");
        isOpened = false;
        interactable.message1 = "Open";
    }

    private void OnDestroy()
    {
        interactable.OnInteraction.RemoveListener(DoInteraction);
        interactable.OnAltInteraction.RemoveListener(DoAltInteraction);
    }
    void EnableCollider()
    {
        doorCollider.enabled = true;
    }
    void DisableCollider()
    {
        doorCollider.enabled = false;
    }
}

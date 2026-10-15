using UnityEngine;
public class DoorScript : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] Collider doorCollider;

    [field: SerializeField] public bool isOpen { get; private set; } = false;
    [field: SerializeField] public bool isLocked { get; private set; } = false;

    public bool TryLock(bool canLock = true)
    {
        if(isOpen || !canLock)
            return false;

        isLocked = true;
        return true;
    }
    public bool TryUnlock(bool canUnlock = true)
    {
        if(!canUnlock)
            return false;

        isLocked = false;
        return true;
    }

    public bool TryOpen()
    {
        if (isLocked)
            return false;

        Open();
        return true;
    }


    void Open()
    {
        doorAnimator.Play("OpenDoorAnimation");
        isOpen = true;
    }
    public void Close()
    {
        doorAnimator.Play("CloseDoorAnimation");
        isOpen = false;
    }

    public void ForceOpen()
    {
        Open();
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

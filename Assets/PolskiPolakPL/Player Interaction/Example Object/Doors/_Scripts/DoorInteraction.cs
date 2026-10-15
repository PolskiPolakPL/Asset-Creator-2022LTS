using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] InteractableObject interaction;
    [SerializeField] DoorScript door;

    private void Awake()
    {
        interaction.OnInteraction.AddListener(TryOpenCloseDoor);
        interaction.OnAltInteraction.AddListener(TryLockUnlockDoor);

        interaction.message1 = door.isOpen ? "Close" : "Open";
        interaction.message2 = door.isLocked ? "Unlock" : "Lock";
    }

    void TryOpenCloseDoor()
    {
        if (door.isOpen)
        {
            door.Close();
            interaction.message1 = "Open";
            return;
        }

        if (door.TryOpen())
        {
            interaction.message1 = "Close";
            return;
        }
    }

    void TryLockUnlockDoor()
    {
        if (door.isLocked)
        {
            if (!door.TryUnlock())
                return;

            interaction.message2 = "Lock";
            return;
        }

        if (door.TryLock())
        {
            interaction.message2 = "Unlock";
            return;
        }
    }

    private void OnDestroy()
    {
        interaction.OnInteraction.RemoveListener(TryOpenCloseDoor);
        interaction.OnAltInteraction.RemoveListener(TryLockUnlockDoor);
    }
}

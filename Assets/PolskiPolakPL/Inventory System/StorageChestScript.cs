using UnityEngine;

[System.Serializable]
public class ChestItem
{
    public ItemData Item;
    public int amount;
}

public class StorageChestScript : MonoBehaviour
{
    Interactable interactable;

    [SerializeField] ChestItem[] storedItems;

    static GameObject chestPanel;
    static ItemSlot[] chestSlots;
    static int maxChestSize;
    int chestSize;

    bool isOpen;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += OpenCloseChest;
    }

    private void Start()
    {
        if (!chestPanel)
        {
            chestPanel = InventorySystem.Instance.chestPanel;
            chestSlots = chestPanel.GetComponentsInChildren<ItemSlot>(true);
        }
            maxChestSize = chestSlots.Length;
            chestSize = Mathf.Min(storedItems.Length, maxChestSize);
        foreach (ItemSlot slot in chestSlots)
        {
            slot.gameObject.SetActive(false);
        }
            chestPanel.SetActive(false);

        for(int i = 0; i < chestSize; i++)
        {
            storedItems[i] = new ChestItem();
        }
    }

    void OpenCloseChest()
    {
        if (!isOpen)
            Open();
        else
            Close();
    }

    void Open()
    {
        isOpen = true;
        interactable.message = "Close";
        chestPanel.SetActive(true);

        ChestItem storedItem;
        for(int i = 0;  i < chestSize; i++)
        {
            storedItem = storedItems[i];
            if (storedItem.Item)
                chestSlots[i].SetItem(storedItem.Item, storedItem.amount);
            else
                chestSlots[i].ClearSlot();
            chestSlots[i].gameObject.SetActive(true);
        }
        InventorySystem.Instance.ToggleInventoryPanel(isOpen);
    }

    public void Close()
    {
        if (!isOpen) return;

        for(int i = 0; i < chestSize; i++)
        {
            if (chestSlots[i].HasItem())
            {
                storedItems[i].Item = chestSlots[i].GetItem();
                storedItems[i].amount = chestSlots[i].GetAmount();
            }
            else
            {
                storedItems[i].Item = null;
                storedItems[i].amount = 0;
            }
            chestSlots[i].gameObject.SetActive(false);
        }

        isOpen = false;
        interactable.message = "Open";
        chestPanel.SetActive(false);
        InventorySystem.Instance.ToggleInventoryPanel(false);
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseChest;
    }
}

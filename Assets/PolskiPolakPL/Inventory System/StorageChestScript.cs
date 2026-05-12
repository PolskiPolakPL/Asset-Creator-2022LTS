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
    static ItemSlot[] chestUISlots;
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
            chestUISlots = chestPanel.GetComponentsInChildren<ItemSlot>(true);
            foreach (ItemSlot slot in chestUISlots)
            {
                slot.gameObject.SetActive(false);
            }
        }

        chestSize = Mathf.Min(storedItems.Length, chestUISlots.Length);
        chestPanel.SetActive(false);
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

        GetItemsFromChest();
        InventorySystem.Instance.ToggleInventoryPanel(isOpen);
    }

    public void Close()
    {
        if (!isOpen) return;

        StoreItemsInChest();

        isOpen = false;
        interactable.message = "Open";
        chestPanel.SetActive(false);
        InventorySystem.Instance.ToggleInventoryPanel(false);
    }

    void StoreItemsInChest()
    {
        for (int i = 0; i < chestSize; i++)
        {
            if (chestUISlots[i].HasItem())
            {
                storedItems[i].Item = chestUISlots[i].GetItem();
                storedItems[i].amount = chestUISlots[i].GetAmount();
            }
            else
            {
                storedItems[i].Item = null;
                storedItems[i].amount = 0;
            }
            chestUISlots[i].gameObject.SetActive(false);
        }
    }

    void GetItemsFromChest()
    {
        ChestItem storedItem;
        for (int i = 0; i < chestSize; i++)
        {
            storedItem = storedItems[i];
            if (storedItem.Item)
                chestUISlots[i].SetItem(storedItem.Item, storedItem.amount);
            else
                chestUISlots[i].ClearSlot();
            chestUISlots[i].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseChest;
    }
}

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

    public int chestSize = 32;
    public ChestItem[] storedItems;

    static GameObject chestPanel;
    static ItemSlot[] chestSlots;

    bool isOpen;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += OpenCloseChest;
    }

    private void Start()
    {
        storedItems = new ChestItem[chestSize];
        if (!chestPanel)
        {
            chestPanel = InventorySystem.Instance.chestPanel;
            chestSlots = chestPanel.GetComponentsInChildren<ItemSlot>(true);
            chestPanel.SetActive(false);
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
        chestPanel.SetActive(true);

        ChestItem storedItem;
        for(int i = 0;  i<chestSlots.Length; i++)
        {
            storedItem = storedItems[i];
            if (storedItem.Item)
                chestSlots[i].SetItem(storedItem.Item, storedItem.amount);
            else
                chestSlots[i].ClearSlot();

        }
        InventorySystem.Instance.ToggleInventoryPanel(isOpen);
    }

    public void Close()
    {
        if (!isOpen) return;

        for(int i = 0; i < chestSlots.Length; i++)
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
        }

        isOpen = false;
        chestPanel.SetActive(false);
        InventorySystem.Instance.ToggleInventoryPanel(false);
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseChest;
    }
}

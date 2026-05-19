using UnityEngine;

[System.Serializable]
public class ChestItem
{
    public ItemData Item;
    public int amount;
}

public class ItemChestScript : MonoBehaviour
{
    Interactable interactable;

    [SerializeField] ChestItem[] storedItems;

    GameObject ChestUIPanel;
    public static ItemSlot[] ChestUISlots { get; private set; }
    int chestSize;

    bool isOpen;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += OpenCloseChest;
    }

    private void Start()
    {
        if (!ChestUIPanel)
        {
            ChestUIPanel = InventoryUIManager.Instance.ChestUIPanel;
            ChestUISlots = ChestUIPanel.GetComponentsInChildren<ItemSlot>(true);
            foreach (ItemSlot slot in ChestUISlots)
            {
                slot.gameObject.SetActive(false);
            }
        }

        chestSize = Mathf.Min(storedItems.Length, ChestUISlots.Length);
        ChestUIPanel.SetActive(false);
        InventoryUIManager.Instance.OnHideInventoryPanel.AddListener(Close);
    }

    void OpenCloseChest()
    {
        if (!isOpen)
            Open();
        else
            Close();
        InventoryUIManager.Instance.ToggleInventoryPanel(isOpen);
    }

    void Open()
    {
        isOpen = true;
        interactable.message = "Close chest";
        ChestUIPanel.SetActive(true);

        ReadChest();
    }

    public void Close()
    {
        if (!isOpen) return;

        WriteChest();

        isOpen = false;
        interactable.message = "Open chest";
        ChestUIPanel.SetActive(false);
    }

    void WriteChest()
    {
        for (int i = 0; i < chestSize; i++)
        {
            if (ChestUISlots[i].HasItem())
            {
                storedItems[i].Item = ChestUISlots[i].GetItem();
                storedItems[i].amount = ChestUISlots[i].GetAmount();
            }
            else
            {
                storedItems[i].Item = null;
                storedItems[i].amount = 0;
            }
            ChestUISlots[i].gameObject.SetActive(false);
        }
    }

    void ReadChest()
    {
        ChestItem storedItem;
        for (int i = 0; i < chestSize; i++)
        {
            storedItem = storedItems[i];
            if (storedItem.Item)
                ChestUISlots[i].SetItem(storedItem.Item, storedItem.amount);
            else
                ChestUISlots[i].ClearSlot();
            ChestUISlots[i].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseChest;
        InventoryUIManager.Instance.OnHideInventoryPanel.RemoveListener(Close);
    }
}

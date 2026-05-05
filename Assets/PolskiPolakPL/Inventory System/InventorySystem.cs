using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Singleton Instance
    public static InventorySystem Instance {  get; private set; }

    [Header("Slots Reference")]
    [SerializeField] Transform hotbarSlotsParent;
    [SerializeField] Transform backpackSlotsParent;
    // Slots Lists
    List<ItemSlot> hotbarSlots = new List<ItemSlot>();
    List<ItemSlot> backpackSlots = new List<ItemSlot>();
    List<ItemSlot> playerInventorySlots = new List<ItemSlot>();

    [Header("Player Hand")]
    [SerializeField] Transform playerHand;
    [SerializeField] float throwingForce = 5;

    [Header("UI")]
    [SerializeField] GameObject playerInventoryPanel;
    [SerializeField][Range(0,1)] float normalOpacity = .6f;
    [SerializeField][Range(0, 1)] float selectedOpacity = .8f;

    int selectedIndex = 0;


    private void Awake()
    {
        if(Instance && Instance!=this)
            Destroy(gameObject);
        else
            Instance = this;

        InitializeLists();
    }

    void InitializeLists()
    {
        if(!hotbarSlotsParent && !backpackSlotsParent)
        {
            Debug.LogWarning($"[{this}]: None of the slots parent was attached! Attach at least one slot parent.");
            return;
        }
        if (hotbarSlotsParent)
        {
            hotbarSlots.AddRange(hotbarSlotsParent.GetComponentsInChildren<ItemSlot>());
            playerInventorySlots.AddRange(hotbarSlots);
        }
        if(backpackSlotsParent)
        {
            backpackSlots.AddRange(backpackSlotsParent.GetComponentsInChildren<ItemSlot>());
            playerInventorySlots.AddRange(backpackSlots);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryPanel(playerInventoryPanel.activeInHierarchy);
        }
        HandleHotbarSelection();
        HandleItemDropping();
    }

    void ToggleInventoryPanel(bool toggle)
    {
        playerInventoryPanel.SetActive(!toggle);
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        //Try putting item in selected slot
        if(TryAddItemToSelectedSlot(item, amount, out int remaining))
            return true;

        amount = remaining;

        //Try putting item in any slot with the same item
        if(TryFillExistingSlots(item, amount, out remaining))
            return true;

        amount = remaining;

        // Add item to empty Slot
        if(TryFillEmptySlots(item, amount, out remaining))
            return true;

        //Inventory full
        Debug.Log($"Inventory is full! Could not add {remaining} of {item.DisplayName}");
        return false;
    }
    #region Adding item to Inventory
    bool TryAddItemToSelectedSlot(ItemData item, int amount, out int remaining)
    {
        remaining = amount;

        ItemSlot selectedSlot = hotbarSlots[selectedIndex];

        if (!selectedSlot.HasItem())
            AddAmountToSlot(item, amount, selectedSlot, out remaining);

        else if (selectedSlot.GetItem() == item)
            IncreaseAountInSlot(amount, selectedSlot, out remaining);

        return (remaining <= 0) ? true : false;
    }

    bool TryFillExistingSlots(ItemData item, int amount, out int remaining)
    {
        remaining = amount;
        foreach (ItemSlot slot in playerInventorySlots)
        {
            if (slot.HasItem() && slot.GetItem() == item)
            {
                IncreaseAountInSlot(amount, slot, out remaining);
                if (remaining <= 0)
                    return true;
            }
        }
        return false;
    }

    bool TryFillEmptySlots(ItemData item, int amount, out int remaining)
    {
        remaining = amount;
        foreach (ItemSlot slot in playerInventorySlots)
        {
            if (!slot.HasItem())
            {
                AddAmountToSlot(item, amount, slot, out remaining);
                if (remaining <= 0)
                    return true;
            }
        }
        return false;
    }

    void IncreaseAountInSlot(int amount, ItemSlot slot, out int remaining)
    {
        remaining = amount;
        int currentAmount = slot.GetAmount();
        int maxStack = slot.GetItem().StackSize;
        if (currentAmount < maxStack)
        {
            int amountToAdd = Mathf.Min(maxStack - currentAmount, remaining);

            slot.SetItem(slot.GetItem(), currentAmount + amountToAdd);
            remaining -= amountToAdd;
        }
    }

    void AddAmountToSlot(ItemData itemToAdd, int amount, ItemSlot slot, out int remaining)
    {
        remaining = amount;
        int amountToPlace = Mathf.Min(itemToAdd.StackSize, amount);
        slot.SetItem(itemToAdd, amountToPlace);
        remaining -= amountToPlace;
    }

    #endregion

    void HandleHotbarSelection()
    {
        // scroll down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedIndex++;
            selectedIndex %= hotbarSlots.Count;
        }
        //scroll up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedIndex <= 0)
                selectedIndex = hotbarSlots.Count - 1;
            else
                selectedIndex--;
        }
        // 1-X key-binds
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
                selectedIndex = i;
        }

        //item opacity + item in hand
        UpdateSelectedSlot();
        SetItemInHand();
    }

    void UpdateSelectedSlot()
    {
        Image bgImage;
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            bgImage = hotbarSlots[i].GetComponent<Image>();
            if (i == selectedIndex)
                bgImage.color = new Color(0, 0, 0, selectedOpacity);
            else
                bgImage.color = new Color(0,0,0,normalOpacity);
        }
    }

    void SetItemInHand()
    {
        ItemSlot selectedSlot = hotbarSlots[selectedIndex];
        foreach (Transform child in playerHand)
            Destroy(child.gameObject);
        if (selectedSlot.HasItem())
            Instantiate(selectedSlot.GetItem().HandPrefab, playerHand);
    }

    void HandleItemDropping()
    {
        if(!Input.GetKeyDown(KeyCode.G))
            return;
        ItemSlot selectedSlot = hotbarSlots[selectedIndex];
        if (!selectedSlot.HasItem())
            return;

        ItemData itemData = selectedSlot.GetItem();
        // Remove hand item prefab
        GameObject handPrefab = playerHand.GetChild(0).gameObject;
        Destroy(handPrefab);

        // Create and throw world item prefab
        GameObject droppedItemGo = Instantiate(itemData.WorldPrefab, playerHand.position, playerHand.rotation);
        droppedItemGo.GetComponent<Rigidbody>().AddForce(playerHand.forward * throwingForce, ForceMode.Impulse);

        selectedSlot.ClearSlot();
    }

}

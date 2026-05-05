using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Singleton Instance
    public static InventorySystem Instance {  get; private set; }

    [Header("Slots Parents")]
    [SerializeField] Transform hotbarSlotsParent;
    [SerializeField] Transform backpackSlotsParent;

    // Slots Lists
    List<ItemSlot> hotbarSlots = new List<ItemSlot>();
    List<ItemSlot> backpackSlots = new List<ItemSlot>();
    List<ItemSlot> playerInventorySlots = new List<ItemSlot>();

    [Header("Player Hand")]
    [SerializeField] Transform playerHand;
    [SerializeField] KeyCode dropKey = KeyCode.G;
    [SerializeField] float throwingForce = 5;

    [Header("UI")]
    [SerializeField] GameObject playerInventoryPanel;
    [SerializeField][Range(0,1)] float normalOpacity = .6f;
    [SerializeField][Range(0, 1)] float selectedOpacity = .8f;
    public UnityEvent<bool> OnInventoryToggle;

    int selectedIndex = 0;
    ItemSlot selectedSlot;


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
            ToggleInventoryPanel(!playerInventoryPanel.activeInHierarchy);
        }

        //StartDrag
        //UpdateDragPosition
        //EndDrag

        HandleHotbarSelection();
        HandleItemDropping();
    }

    void ToggleInventoryPanel(bool toggle)
    {
        playerInventoryPanel.SetActive(toggle);
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;
        OnInventoryToggle?.Invoke(!toggle);
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
        EquipHandItem();
    }

    void UpdateSelectedSlot()
    {
        selectedSlot = hotbarSlots[selectedIndex];
        Image bgImage;
        foreach (ItemSlot slot in hotbarSlots)
        {
            bgImage = slot.GetComponent<Image>();
            bgImage.color = (slot == selectedSlot) ? new Color(0, 0, 0, selectedOpacity) : new Color(0, 0, 0, normalOpacity);
        }
    }

    void EquipHandItem()
    {
        foreach (Transform child in playerHand)
            Destroy(child.gameObject);
        if (selectedSlot.HasItem())
            Instantiate(selectedSlot.GetItem().HandPrefab, playerHand);
    }

    void HandleItemDropping()
    {
        if(!Input.GetKeyDown(dropKey))
            return;
        if (!selectedSlot.HasItem())
            return;

        ItemData selectedItem = selectedSlot.GetItem();
        if (!selectedItem.WorldPrefab)
            return;

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Drop all items
            DropItem(selectedItem, selectedSlot.GetAmount());
            // Remove hand item prefab
            EquipHandItem();
            return;
        }
        // drop one item
        DropItem(selectedItem);
    }

    void DropItem(ItemData item, int dropAmount = 1)
    {
        // Create and throw world item prefab
        GameObject droppedItemGO = Instantiate(item.WorldPrefab, playerHand.position, playerHand.rotation);
        droppedItemGO.GetComponent<Rigidbody>().AddForce(playerHand.forward * throwingForce, ForceMode.Impulse);
        // Set dropped amount to match
        droppedItemGO.GetComponent<ItemScript>().amount = dropAmount;
        // remove amount from slot
        selectedSlot.RemoveAmount(dropAmount);
    }

}

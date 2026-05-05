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
        HandleHotbarSelection();
        HandleItemDropping();
    }

    public bool AddItem(ItemData item)
    {
        ItemSlot selectedSlot = hotbarSlots[selectedIndex];
        //Try putting item in selected slot
        if (!selectedSlot.HasItem())
        {
            selectedSlot.SetItem(item);
            return true;
        }
        //Try putting item in any slot
        int i = 0;
        foreach (ItemSlot slot in hotbarSlots)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(item);
                return true;
            }
            i++;
        }
        Debug.Log("Inventory full!");
        return false;
    }

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
            Instantiate(selectedSlot.GetItemData().HandPrefab, playerHand);
    }

    void HandleItemDropping()
    {
        if(!Input.GetKeyDown(KeyCode.G))
            return;
        ItemSlot selectedSlot = hotbarSlots[selectedIndex];
        if (!selectedSlot.HasItem())
            return;

        ItemData itemData = selectedSlot.GetItemData();
        // Remove hand item prefab
        GameObject handPrefab = playerHand.GetChild(0).gameObject;
        Destroy(handPrefab);

        // Create and throw world item prefab
        GameObject droppedItemGo = Instantiate(itemData.WorldPrefab, playerHand.position, playerHand.rotation);
        droppedItemGo.GetComponent<Rigidbody>().AddForce(playerHand.forward * throwingForce, ForceMode.Impulse);

        selectedSlot.ClearSlot();
    }

}

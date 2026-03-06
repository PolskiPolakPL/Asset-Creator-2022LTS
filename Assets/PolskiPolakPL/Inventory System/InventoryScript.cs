using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript Instance {  get; private set; }

    [SerializeField][Min(1)] int itemArraySize = 1;
    public GameObject playerHotbarGO;

    [SerializeField] Transform playerHand;
    [SerializeField] float throwingForce;
    float normalOpacity = .6f;
    float selectedOpacity = .8f;

    public int selectedIndex { get; private set; } = 0;
    public List<ItemSlot> itemSlots { get; private set; } = new List<ItemSlot>();

    private void Awake()
    {
        Application.targetFrameRate = 600;
        if(Instance && Instance!=this)
            Destroy(gameObject);
        else
            Instance = this;

        itemSlots.AddRange(playerHotbarGO.GetComponentsInChildren<ItemSlot>());
    }

    private void Update()
    {
        HandleHotbarSelection();
        HandleItemDropping();
    }

    public bool AddItem(ItemData item)
    {
        ItemSlot selectedSlot = itemSlots[selectedIndex];
        //Try putting item in selected slot
        if (!selectedSlot.HasItem())
        {
            selectedSlot.SetItem(item, itemArraySize);
            Instantiate(item.HandPrefab, playerHand.GetChild(selectedIndex));
            return true;
        }
        //Try putting item in any slot
        int i = 0;
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(item, itemArraySize);
                Instantiate(item.HandPrefab, playerHand.GetChild(i));
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
            selectedIndex %= itemSlots.Count;
        }
        //scroll up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedIndex <= 0)
                selectedIndex = itemSlots.Count - 1;
            else
                selectedIndex--;
        }
        // 1-X key-binds
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
                selectedIndex = i;
        }

        //item opacity + item in hand
        UpdateSelectedItem();
    }

    void UpdateSelectedItem()
    {
        Image bgImage;
        for (int i = 0; i < itemSlots.Count; i++)
        {
            bgImage = itemSlots[i].GetComponent<Image>();
            if (!bgImage)
                return;
            if (i == selectedIndex)
            {
                bgImage.color = new Color(0, 0, 0, selectedOpacity);
                playerHand.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                bgImage.color = new Color(0,0,0,normalOpacity);
                playerHand.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void HandleItemDropping()
    {
        if(!Input.GetKeyDown(KeyCode.G))
            return;
        ItemSlot selectedSlot = itemSlots[selectedIndex];
        if (!selectedSlot.HasItem())
            return;

        ItemData itemData = selectedSlot.GetItemData();
        // Remove hand item prefab
        GameObject handPrefab = playerHand.GetChild(selectedIndex).GetChild(0).gameObject;
        Destroy(handPrefab);

        // Create and throw world item prefab
        GameObject droppedItemGo = Instantiate(itemData.WorldPrefab, playerHand.position, playerHand.rotation);
        droppedItemGo.GetComponent<Rigidbody>().AddForce(playerHand.forward * throwingForce, ForceMode.Impulse);

        selectedSlot.ClearSlot();
    }
}

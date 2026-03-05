using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField][Min(1)] int itemArraySize = 1;
    public GameObject playerHotbarGO;
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField] List<ItemData> debugItemData = new List<ItemData>();

    private void Awake()
    {
        itemSlots.AddRange(playerHotbarGO.GetComponentsInChildren<ItemSlot>());
    }

    private void Start()
    {
        foreach(ItemData item in debugItemData)
            AddItem(item);
    }

    public bool AddItem(ItemData item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(item, itemArraySize);
                return true;
            }
        }
        Debug.Log("Inventory full!");
        return false;
    }
    public void RemoveItem(ItemData item)
    {

    }

    
}

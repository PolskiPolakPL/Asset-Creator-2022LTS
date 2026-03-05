using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript Instance {  get; private set; }


    [SerializeField][Min(1)] int itemArraySize = 1;
    public GameObject playerHotbarGO;
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Awake()
    {
        if(Instance && Instance!=this)
            Destroy(gameObject);
        else
            Instance = this;

        itemSlots.AddRange(playerHotbarGO.GetComponentsInChildren<ItemSlot>());
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

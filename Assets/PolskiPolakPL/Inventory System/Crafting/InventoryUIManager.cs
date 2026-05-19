using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class InventoryUIManager : MonoBehaviour
{
    InventorySystem inventory;


    [field: Header("Selected Slot BG")]
    [Range(0, 1)] public float normalOpacity = .6f;
    [Range(0, 1)] public float selectedOpacity = .8f;

    [Header("Item Description Panel")]
    [SerializeField] DescriptionPanelScript descrPanelScr;

    [Header("Item Drag")]
    [SerializeField] ItemDragScript itemDragScr;

    [Header("Inventory Panel")]
    [SerializeField] GameObject playerInventoryPanel;
    public UnityEvent<bool> OnInventoryToggle;

    [Header("Item Chest Panel")]
    public GameObject chestPanel;

    public static InventoryUIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        inventory = InventorySystem.Instance;
    }

    public void UpdateSelectedSlot(ItemSlot selectedSlot)
    {
        Image bgImage;
        foreach (ItemSlot slot in inventory.hotbarSlots)
        {
            bgImage = slot.bgImage;
            bgImage.color = (slot == selectedSlot) ? new Color(0, 0, 0, selectedOpacity) : new Color(0, 0, 0, normalOpacity);
        }
    }
}

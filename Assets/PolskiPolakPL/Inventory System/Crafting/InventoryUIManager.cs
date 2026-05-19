using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class InventoryUIManager : MonoBehaviour
{
    [field: Header("Selected Slot BG")]
    [Range(0, 1)] public float normalOpacity = .6f;
    [Range(0, 1)] public float selectedOpacity = .8f;

    [Header("Item Drag")]
    [SerializeField] ItemDragScript itemDragScr;

    [Header("Inventory UI Panels")]
    [SerializeField] DescriptionPanelScript descrPanelScr;
    [field: SerializeField] public GameObject ChestUIPanel { get; private set; }
    [SerializeField] GameObject playerInventoryPanel;

    public UnityEvent OnShowInventoryPanel;
    public UnityEvent OnHideInventoryPanel;


    InventorySystem inventory;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryPanel(!playerInventoryPanel.activeInHierarchy);
        }

        if (itemDragScr)
        {
            itemDragScr.HandleItemDrag();
            if (descrPanelScr)
                descrPanelScr.HandleDescriptionPanel(itemDragScr.GetHoveredSlot());
        }

        UpdateSelectedSlot(inventory.selectedSlot);
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

    public void ToggleInventoryPanel(bool toggle)
    {
        // Handle Inventory Panel
        playerInventoryPanel.SetActive(toggle);

        // Handle Cursor
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;

        if (toggle)
        {
            OnShowInventoryPanel?.Invoke();
            return;
        }
        OnHideInventoryPanel?.Invoke();
    }
}

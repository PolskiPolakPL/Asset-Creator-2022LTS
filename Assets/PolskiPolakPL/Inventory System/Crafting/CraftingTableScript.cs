using UnityEngine;

public class CraftingTableScript : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    [SerializeField] CraftingScript craftingScr;
    static GameObject craftingPanel;
    bool isOpen = false;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        if(!craftingPanel)
            craftingPanel = craftingScr.gameObject;
        interactable.OnInteraction += OpenCloseCrafting;
    }

    private void Start()
    {
        InventoryUIManager.Instance.OnHideInventoryPanel.AddListener(Close);
    }

    void OpenCloseCrafting()
    {
        if (isOpen)
            Close();
        else
            Open();
        InventoryUIManager.Instance.ToggleInventoryPanel(isOpen);
    }

    void Close()
    {
        if (!isOpen)
            return;
        isOpen = false;
        interactable.message = "Open crafting";
        craftingPanel.SetActive(false);
    }

    void Open()
    {
        isOpen = true;
        interactable.message = "Close crafting";
        craftingPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseCrafting;
        InventoryUIManager.Instance.OnHideInventoryPanel.RemoveListener(Close);
    }
}

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


    void OpenCloseCrafting()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    void Close()
    {
        InventorySystem.Instance.ToggleInventoryPanel(false);
        craftingPanel.SetActive(false);
        isOpen = false;
    }

    void Open()
    {
        isOpen = true;
        craftingPanel.SetActive(true);
        InventorySystem.Instance.ToggleInventoryPanel(true);
    }

    private void OnDestroy()
    {
        interactable.OnInteraction -= OpenCloseCrafting;
    }
}

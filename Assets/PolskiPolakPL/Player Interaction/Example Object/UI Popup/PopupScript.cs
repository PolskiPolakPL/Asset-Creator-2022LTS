using UnityEngine;


[RequireComponent(typeof(Interactable))]
public class PopupScript : MonoBehaviour
{
    [SerializeField] GameObject popupWindow;
    Interactable interactable;
    FPSMovement playerMovement;
    FPSLook playerLook;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteraction += ShowPopup;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameManager.Instance.Player.GetComponent<FPSMovement>();
        playerLook = GameManager.Instance.Player.GetComponent<FPSLook>();
    }

    public void ShowPopup()
    {
        playerMovement.enabled = false;
        playerLook.enabled = false;
        popupWindow.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void HidePopup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        popupWindow.SetActive(false);
        playerLook.enabled = true;
        playerMovement.enabled = true;
    }

    private void OnDestroy()
    {
        HidePopup();
        interactable.OnInteraction -= ShowPopup;
    }
}

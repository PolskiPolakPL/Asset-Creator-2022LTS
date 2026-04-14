using UnityEngine;
using TMPro;
/// <summary>
/// Interaction System made with this
/// <seealso href="https://youtu.be/b7Yf6BFx6js">tutorial</seealso>
/// </summary>
public class InteractionUIManager : MonoBehaviour
{
    //Singleton statement
    public static InteractionUIManager Instance;
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    //Attributes
    [SerializeField] TMP_Text interactionMessage;

    public void DisplayInteractionText(string message)
    {
        interactionMessage.text = message;
        interactionMessage.gameObject.SetActive(true);
    }
    public void HideInteractionText()
    {
        interactionMessage.gameObject.SetActive(false);
    }
}

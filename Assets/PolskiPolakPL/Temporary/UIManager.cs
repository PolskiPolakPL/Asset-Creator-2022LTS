using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    [SerializeField] TMP_Text GameMessageTextField;
    [SerializeField] GameObject PlayerUIPrefab;

    public void UpdateGameMessage(string newMessage)
    {
        if (!GameMessageTextField)
            return;
        GameMessageTextField.text = newMessage;
    }


    public void CreatePlayerUI(string PlayerLabelText)
    {
        GameObject playerUILabel = Instantiate(PlayerUIPrefab, PlayerUIPrefab.transform.parent);
        playerUILabel.GetComponent<TMP_Text>().text = PlayerLabelText;
    }



}

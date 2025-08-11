using UnityEngine;
using TMPro;

public class GameVersionText : MonoBehaviour
{
    TMP_Text versionTextField;
    // Start is called before the first frame update
    void Start()
    {
        versionTextField = GetComponent<TMP_Text>();
        versionTextField.text = $"Game Version: {AppManager.GameVersion}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class PlayerVitals : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] ConditionBar HPBar;
    [SerializeField] float maxStamina;
    [SerializeField] ConditionBar StaminaBar;
    [SerializeField] float maxSanity;
    [SerializeField] ConditionBar SanityBar;


    Condition Health;
    Condition Stamina;
    Condition Sanity;

    Condition targetMetric;

    string red = "#FF3030";
    string blue = "#10FFFF";
    string yellow = "#FFFF10";

    private void Awake()
    {
        Health = new Condition(maxHP);
        HPBar.AttachCondition(Health);

        Stamina = new Condition(maxStamina);
        StaminaBar.AttachCondition(Stamina);

        Sanity = new Condition(maxSanity);
        SanityBar.AttachCondition(Sanity);

    }
    // Start is called before the first frame update
    void Start()
    {
        targetMetric = Health;
        DisplayPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            targetMetric.Gain(13);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            targetMetric.Loose(13);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            targetMetric = Health;
            Debug.Log($"Switched to Health");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            targetMetric = Stamina;
            Debug.Log($"Switched to Stamina");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            targetMetric = Sanity;
            Debug.Log($"Switched to Sanity");
        }
    }


    void DisplayPlayerStats()
    {
        
        Debug.Log($"<b>Health: </b><color={red}>{Health.CurrentVal}/{Health.MaxVal}</color>\t" +
            $"<b>Stamina: </b><color={yellow}>{Stamina.CurrentVal}/{Stamina.MaxVal}</color>\t" +
            $"<b>Sanity: </b><color={blue}>{Sanity.CurrentVal}/{Sanity.MaxVal}</color>");
    }

    void DisplayeStamina()
    {
        Debug.Log($"<b>Stamina: </b><color={yellow}>{Stamina.CurrentVal}/{Stamina.MaxVal}</color>\t");
    }

    void DisplayHP()
    {
        Debug.Log($"<b>Health: </b><color={red}>{Health.CurrentVal}/{Health.MaxVal}</color>\t");
    }

    void DisplaySanity()
    {
        Debug.Log($"<b>Sanity: </b><color={blue}>{Sanity.CurrentVal}/{Sanity.MaxVal}</color>");
    }
}

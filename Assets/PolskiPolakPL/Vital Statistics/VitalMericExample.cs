using UnityEngine;

public class VitalMericExample : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float maxStamina;
    [SerializeField] float maxSanity;


    VitalsMetric Health;
    VitalsMetric Stamina;
    VitalsMetric Sanity;

    VitalsMetric targetMetric;

    string red = "#FF3030";
    string blue = "#10FFFF";
    string yellow = "#FFFF10";

    private void Awake()
    {
        Health = new VitalsMetric(maxHP);
        Health.OnValueChange += DisplayHP;
        Health.OnFilled += DisplayFillStat;
        Health.OnDrained += DisplayEmptyStats;

        Stamina = new VitalsMetric(maxStamina);
        Stamina.OnValueChange += DisplayeStamina;
        Stamina.OnFilled += DisplayFillStat;
        Stamina.OnDrained += DisplayEmptyStats;

        Sanity = new VitalsMetric(maxSanity);
        Sanity.OnValueChange += DisplaySanity;
        Sanity.OnFilled += DisplayFillStat;
        Sanity.OnDrained += DisplayEmptyStats;

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
            targetMetric.GainWithoutNotify(13);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            targetMetric.Loose(13);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            targetMetric = Health;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            targetMetric = Stamina;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            targetMetric = Sanity;
        }
    }

    void DisplayFillStat()
    {
        Debug.Log("Metric Full!");
    }

    void DisplayEmptyStats()
    {
        Debug.Log("Metric Depleated!");
    }

    void DisplayPlayerStats()
    {
        
        Debug.Log($"<b>Health: </b><color={red}>{Health.Current}/{Health.Max}</color>\t" +
            $"<b>Stamina: </b><color={yellow}>{Stamina.Current}/{Stamina.Max}</color>\t" +
            $"<b>Sanity: </b><color={blue}>{Sanity.Current}/{Sanity.Max}</color>");
    }

    void DisplayeStamina()
    {
        Debug.Log($"<b>Stamina: </b><color={yellow}>{Stamina.Current}/{Stamina.Max}</color>\t");
    }

    void DisplayHP()
    {
        Debug.Log($"<b>Health: </b><color={red}>{Health.Current}/{Health.Max}</color>\t");
    }

    void DisplaySanity()
    {
        Debug.Log($"<b>Sanity: </b><color={blue}>{Sanity.Current}/{Sanity.Max}</color>");
    }
}

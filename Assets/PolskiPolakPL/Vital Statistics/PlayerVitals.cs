using UnityEngine;

public class PlayerVitals : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float maxStamina;
    [SerializeField] float maxSanity;


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
        Health.OnGained += DisplayHP;
        Health.OnLost += DisplayHP;
        Health.OnFilled += DisplayFillStat;
        Health.OnDrained += DisplayEmptyStats;

        Stamina = new Condition(maxStamina);
        Stamina.OnGained += DisplayHP;
        Stamina.OnLost += DisplayHP;
        Stamina.OnFilled += DisplayFillStat;
        Stamina.OnDrained += DisplayEmptyStats;

        Sanity = new Condition(maxSanity);
        Sanity.OnGained += DisplayHP;
        Sanity.OnLost += DisplayHP;
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
            targetMetric.Gain(13);
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

using UnityEngine;

public class VitalMericExample : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float maxStamina;
    [SerializeField] float maxSanity;


    VitalsMetric Health;
    VitalsMetric Stamina;
    VitalsMetric Sanity;

    string red = "#FF3030";
    string blue = "#10FFFF";
    string yellow = "#FFFF10";

    private void Awake()
    {
        Health = new VitalsMetric(maxHP);
        Stamina = new VitalsMetric(maxStamina);
        Sanity = new VitalsMetric(maxSanity);
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
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

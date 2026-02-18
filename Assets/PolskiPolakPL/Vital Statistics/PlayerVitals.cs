using UnityEngine;

public class PlayerVitals : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHP;
    [SerializeField] ConditionBar HPBar;
    [SerializeField] float maxStamina;
    [SerializeField] ConditionBar StaminaBar;
    [SerializeField] float maxSanity;
    [SerializeField] ConditionBar SanityBar;

    public Condition Health { get; private set; }
    public Condition Stamina { get; private set; }
    public Condition Sanity { get; private set; }

    Condition targetMetric;

    string red = "#FF1010";

    private void Awake()
    {
        Health = new Condition(maxHP);
        HPBar.AttachCondition(Health);
        Health.OnDrained += Die;

        Stamina = new Condition(maxStamina);
        StaminaBar.AttachCondition(Stamina);

        Sanity = new Condition(maxSanity);
        SanityBar.AttachCondition(Sanity);

    }
    // Start is called before the first frame update
    void Start()
    {
        targetMetric = Sanity;
    }

    // Update is called once per frame
    void Update()
    {

        HandleDebug();
    }

    void HandleDebug()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            targetMetric.Gain(7);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            targetMetric.Loose(7);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            targetMetric.SetCurrentValue(targetMetric.MinVal);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            targetMetric.SetCurrentValue(targetMetric.MaxVal);
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

    public void TakeDamage(float amount)
    {
        Health.Loose( amount );
    }

    void Die()
    {
        Debug.Log($"<color={red}><b>YOU ARE DEAD!</b></color>");
    }
}

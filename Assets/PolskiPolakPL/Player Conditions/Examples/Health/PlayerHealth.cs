using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHP;
    [SerializeField] ConditionBar HPBar;

    public Condition health { get; private set; }

    string red = "#FF1010";

    private void Awake()
    {
        health = new Condition(maxHP);
        HPBar.AttachCondition(health);
        health.OnDrained += Die;

    }

    private void Update()
    {
        health.Regen(1);
    }

    public void TakeDamage(float amount)
    {
        health.Loose( amount );
    }

    void Die()
    {
        Debug.Log($"<color={red}><b>YOU ARE DEAD!</b></color>");
    }
}

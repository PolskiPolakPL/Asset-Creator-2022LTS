using UnityEngine;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] UnitData unitData;

    private void Awake()
    {

    }

    UnitData GetUnitData()
    {
        return unitData;
    }
    public void TakeDamage(float amount)
    {

    }
}

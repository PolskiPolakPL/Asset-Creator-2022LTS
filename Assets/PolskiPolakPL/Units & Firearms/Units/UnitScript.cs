using UnityEngine;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] UnitData unitData;

    private void Awake()
    {

    }

    SquadData GetSquadData(Transform target)
    {
        SquadScript squadScript;
        if (!target.TryGetComponent(out squadScript))
        {
            Debug.LogWarning($"squad data not found in {target.name}");
            return null;
        }
        return squadScript.SquadData;
    }
    public void TakeDamage(float amount)
    {

    }
}

using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] UnitData data;
    public NavMeshAgent agent {  get; private set; }
    SquadScript squad;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void SetSquad(SquadScript newSquad)
    {
        squad = newSquad;
    }
    UnitData GetUnitData()
    {
        return data;
    }
    public void TakeDamage(float amount)
    {

    }

    private void OnDestroy()
    {
        if (!squad)
            return;

        squad.RemoveUnit(this);
    }
}

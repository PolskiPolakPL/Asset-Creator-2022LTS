using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] UnitData data;
    public NavMeshAgent agent {  get; private set; }
    [SerializeField] Animator animator;
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    SquadScript squad;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AssignAnimationIDs();
        if(animator)
            animator.SetFloat(_animIDMotionSpeed, 1);
    }



    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    public void Move(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void Update()
    {
        if(animator)
            UpdateAnimations();
    }

    void UpdateAnimations()
    {
        float normalizedAgentSpeed = (agent.velocity.magnitude / agent.speed);
        animator.SetFloat(_animIDSpeed, agent.velocity.magnitude);
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

using UnityEngine;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] UnitData data;
    [field: SerializeField] public UnitAIMovement aiMovement {  get; private set; }
    [SerializeField] Animator animator;
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    SquadScript squad;

    private void Awake()
    {
        if (animator)
        {
            AssignAnimationIDs();
            animator.SetFloat(_animIDMotionSpeed, 1);
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Update()
    {
        if(animator)
            UpdateAnimations();
    }

    void UpdateAnimations()
    {
        animator.SetFloat(_animIDSpeed, aiMovement.GetCurrentSpeed());
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

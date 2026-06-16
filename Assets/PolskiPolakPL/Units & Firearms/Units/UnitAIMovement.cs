using UnityEngine;
using UnityEngine.AI;

public class UnitAIMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float minSpeed = 3f;
    [SerializeField] float slowDownDistance = 5f;

    Quaternion? targetRotation;

    private void Awake()
    {
        if(!agent)
            agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleRotation();
        CheckDestination();
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        if (!agent.hasPath)
        {
            agent.speed = maxSpeed;
            return;
        }

        float distance = agent.remainingDistance;

        if (distance > slowDownDistance)
        {
            agent.speed = maxSpeed;
            return;
        }

        float time = distance / slowDownDistance;
        agent.speed = Mathf.Lerp(minSpeed, maxSpeed, time);
    }

    #region Movement Commands
    public void Move(Vector3 destination, bool updateRotation = true)
    {
        SetAutoRotation(updateRotation);
        agent.SetDestination(destination);
        agent.isStopped = false;
    }

    public void Halt()
    {
        agent.isStopped = true;
    }

    public void Resume()
    {
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }
    #endregion

    #region Rotation Commands
    public void LookAt(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
            return;

        targetRotation = Quaternion.LookRotation(direction);
    }

    public void Face(Vector3 direction)
    {
        direction.y = 0;
        direction.Normalize();

        if (direction.sqrMagnitude <= 0.001f)
            return;

        targetRotation = Quaternion.LookRotation(direction);
    }

    void RotateTowards(Quaternion targetRotation)
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            transform.rotation = targetRotation;
            this.targetRotation = null;
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (agent.updateRotation)
            return;

        if (!targetRotation.HasValue)
            return;

        RotateTowards(targetRotation.Value);
    }

    public void SetAutoRotation(bool autoRotation)
    {
        agent.updateRotation = autoRotation;

        if(autoRotation)
            targetRotation = null;
    }
    #endregion

    void CheckDestination()
    {
        if (!ReachedDestination())
            return;
        agent.isStopped = true;

        if (agent.velocity.sqrMagnitude < 0.01f)
            agent.ResetPath();
    }

    public bool ReachedDestination()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public float GetCurrentSpeed()
    {
        return agent.velocity.magnitude;
    }

    public float GetMaxSpeed()
    {
        return agent.speed;
    }

    public float GetNormalizedSpeed()
    {
        return (GetCurrentSpeed()/GetMaxSpeed());
    }
}

using UnityEngine;

public class WatcherScript : MonoBehaviour
{
    Visibility visibility;

    [SerializeField] GameObject player;
    [SerializeField] float sanityDamage = 5;
    [SerializeField] float damageDistance = 5;
    [SerializeField] float sanityDrainAmount = 1;
    [SerializeField] float drainDistance = 20;

    SanitySystem sanitySystem;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageDistance);
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, drainDistance);
    }

    private void Awake()
    {
        visibility = GetComponent<Visibility>();
        sanitySystem = player.GetComponent<SanitySystem>();
        visibility.OnBecameVisible += DisableRegen;
        visibility.OnBecameVisible += DealSanityDamage;
        visibility.OnBecameHidden += EnableRegen;
    }

    private void Update()
    {
        if (!visibility.isVisible)
            return;
        if(CheckPlayerInRange(drainDistance))
            DrainSanity();
    }

    void DealSanityDamage()
    {
        if (CheckPlayerInRange(damageDistance))
            sanitySystem.sanity.Loose(sanityDamage);
    }

    bool CheckPlayerInRange(float distance)
    {
        Vector3 playerPosition = player.transform.position;
        return Vector3.Distance(playerPosition, transform.position)<=distance;
    }

    void DrainSanity()
    {
        sanitySystem.sanity.Drain(sanityDrainAmount);
    }

    void DisableRegen()
    {
        sanitySystem.CanRegenerate = false;
    }

    void EnableRegen()
    {
        sanitySystem.CanRegenerate = true;
    }
}

using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.name}!");
        IDamagable target;
        if (other.gameObject.TryGetComponent<IDamagable>(out target))
        {
            target.TakeDamage(damage);
        }
    }
}

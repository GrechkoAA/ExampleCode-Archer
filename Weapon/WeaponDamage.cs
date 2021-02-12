using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void HitTargetWithHealth(Health _health, Transform parent)
    {
        transform.SetParent(parent);
        _health.AppyDamage(_damage, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Health>() is Health health)
        {
            HitTargetWithHealth(health, other.transform);
        }
    }
}
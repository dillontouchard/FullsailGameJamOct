using UnityEngine;

public class Stake : MonoBehaviour, IDamage
{
    [SerializeField] int damageAmount;
    [HideInInspector] public int DamageAmount => damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(DamageAmount);
            }
            Destroy(gameObject);
        }
    }
}

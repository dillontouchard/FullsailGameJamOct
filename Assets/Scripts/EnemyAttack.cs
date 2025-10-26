using UnityEngine;

public class EnemyAttack : MonoBehaviour, IDamage
{
    [SerializeField] EnemyPathing pathingScript;
    [SerializeField] Animator enemyController;
    // Head GameObject for raycasting to avoid ray starting at feet
    [SerializeField] GameObject head;
    [SerializeField] BoxCollider armCollider;
    [SerializeField] int damageAmount;
    [SerializeField] int IDamage.DamageAmount => damageAmount;
    private RaycastHit opponent;

    void Start()
    {
        armCollider = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        Ray ray = new Ray(head.transform.position, head.transform.forward * 0.2f);
        Debug.DrawRay(ray.origin, head.transform.forward * 0.4f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.4f, LayerMask.GetMask("Defender")))
        {
            if (hitInfo.collider.CompareTag("Defender"))
            {
                opponent = hitInfo;
                pathingScript.speed = 0;
                enemyController.SetBool("isFighting", true);
            } 
        }
        else if (opponent.collider == null)
        {
            enemyController.SetBool("isFighting", false);
            pathingScript.ResetSpeed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Defender"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                enemyController.SetBool("isFighting", false);
            }
        }
    }

    public void UnhideArmCollider()
    {
        armCollider.enabled = true;
    }

    public void HideArmCollider()
    {
        armCollider.enabled = false;
    }
}

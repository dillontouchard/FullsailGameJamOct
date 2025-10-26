using UnityEditor.Animations;
using UnityEngine;

public class DefenderAttack : MonoBehaviour, IDamage
{
    [SerializeField] DefenderPathing pathingScript;
    [SerializeField] Animator defenderController;
    // Head GameObject for raycasting to avoid ray starting at feet
    [SerializeField] GameObject head;
    // Arm Collider to unhide when attacking, set in animation event
    [SerializeField] BoxCollider armCollider;
    [SerializeField] int damageAmount;
    [SerializeField] int IDamage.DamageAmount => damageAmount;
    private RaycastHit opponent;


    private void Start()
    {
        armCollider = GetComponentInChildren<BoxCollider>();
    }
    void Update()
    {
        Ray ray = new Ray(head.transform.position, head.transform.forward * 0.2f);
        Debug.DrawRay(ray.origin, head.transform.forward * 0.5f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.5f, LayerMask.GetMask("Enemy")))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                opponent = hitInfo;
                defenderController.SetBool("isWalking", false);
                defenderController.SetBool("isFighting", true);
            }
        }
        else if (opponent.collider == null)
        {
            defenderController.SetBool("isFighting", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
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

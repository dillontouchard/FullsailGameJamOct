using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    public int health = 100;
    [SerializeField] Animator animator;
    void IDamageable.TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            animator.SetBool("isFighting", false);
            animator.SetBool("isDead", true);
        }
    }

    public void Dead()
    {
        GameManager.Instance.numOfEnemies--;
        Destroy(gameObject);
    }
}

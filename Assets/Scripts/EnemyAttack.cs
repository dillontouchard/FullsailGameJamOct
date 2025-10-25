using UnityEditor.Animations;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyPathing pathingScript;
    [SerializeField] Animator enemyController;
    // Head GameObject for raycasting to avoid ray starting at feet
    [SerializeField] GameObject head;
    void Update()
    {
        Ray ray = new Ray(head.transform.position, head.transform.forward * 0.2f);
        Debug.DrawRay(ray.origin, head.transform.forward * 0.25f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.25f, LayerMask.GetMask("Defender")))
        {
            if (hitInfo.collider.CompareTag("Defender"))
            {
                pathingScript.speed = 0;
                enemyController.SetBool("isFighting", true);
            }
        }
    }
}

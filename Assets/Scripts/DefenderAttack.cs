using UnityEditor.Animations;
using UnityEngine;

public class DefenderAttack : MonoBehaviour
{
    [SerializeField] DefenderPathing pathingScript;
    [SerializeField] Animator defenderController;
    // Head GameObject for raycasting to avoid ray starting at feet
    [SerializeField] GameObject head;
    void Update()
    {
        Ray ray = new Ray(head.transform.position, head.transform.forward * 0.2f);
        Debug.DrawRay(ray.origin, head.transform.forward * 0.4f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.4f, LayerMask.GetMask("Enemy")))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                defenderController.SetBool("isWalking", false);
                defenderController.SetBool("isFighting", true);
            }
        }
    }
}

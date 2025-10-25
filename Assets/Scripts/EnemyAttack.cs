using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyPathing pathingScript;

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2))
        {
            if (hitInfo.collider.CompareTag("Defender"))
            {
                pathingScript.speed = 0;
            }
        }
    }
}

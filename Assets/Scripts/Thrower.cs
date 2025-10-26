using System.Collections;
using System.Threading;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float throwInterval;
    private float throwTimer;
    void Update()
    {
        throwTimer += Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if(throwTimer > throwInterval)
        {
            if (other.CompareTag("Enemy"))
            {
                StartCoroutine(ThrowProjectileCoroutine(other.transform));
                throwTimer = 0f;
            }
        }
    }

    public IEnumerator ThrowProjectileCoroutine(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Transform projectileTransform = projectile.transform;
        Quaternion projectileRotation = projectileTransform.rotation;
        projectileRotation.x = 90f;
        while (projectile != null && target != null & Vector3.Distance(projectileTransform.position, target.position) > 0.1f)
        {
            float step = projectileSpeed * Time.deltaTime;
            projectileTransform.position = Vector3.MoveTowards(projectileTransform.position, target.position, step);
            yield return null;
        }
        yield break;
    }
}

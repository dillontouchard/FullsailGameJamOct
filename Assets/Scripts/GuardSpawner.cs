using UnityEngine;
using UnityEngine.Splines;

public class GuardSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] defenders;
    [SerializeField] SplineContainer[] splines;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay;
    float spawnTimer;
    Vector3 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = transform.Find("roof-high-flat (1)");
        position.x = spawnPoint.position.x;
        position.y = 0;
        position.z = spawnPoint.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            Instantiate(defenders[0], position, Quaternion.LookRotation(Vector3.back, Vector3.up));
            spawnTimer = 0;
        }
    }
}

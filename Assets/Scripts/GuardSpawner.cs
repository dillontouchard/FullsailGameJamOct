using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class GuardSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] defenders;
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
        // Check how many lanes are taken, if all are taken do not spawn more defenders
        int count = PathManager.Instance.takenPaths.Count();
        if ( count == 3) { return; }
        // Increment spawn timer
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            GiveDefenderLane();
        }
    }

    // Spawns a defender and assigns them to an available lane then resets the spawn timer
    public void GiveDefenderLane()
    {
        foreach (PathManager.LaneType lane in System.Enum.GetValues(typeof(PathManager.LaneType)))
        {
            if (!PathManager.Instance.takenPaths.Contains(lane))
            {
                GameObject newDefender = Instantiate(defenders[0], position, Quaternion.LookRotation(Vector3.back, Vector3.up));
                newDefender.GetComponent<DefenderPathing>().type = lane;
                PathManager.Instance.takenPaths.Add(lane);
                spawnTimer = 0;
                return;
            }
        }
    }
}

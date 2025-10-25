using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyPathing : MonoBehaviour
{
    // Serialize field for spline path
    [SerializeField] private SplineContainer splineContainer;
    // Movement speed along the spline
    public float speed;
    private float speedCopy;
    // Progress along the spline
    private float progress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedCopy = speed;
        SetSpawnOnSpline();
    }

    // Update is called once per frame
    void Update()
    {
        if (splineContainer == null) { return; }
        progress += speed * Time.deltaTime / splineContainer.Spline.GetLength();
        // Destroy the enemy if it has reached the end of the spline
        if (progress > 1f)
        {
            Destroy(gameObject);
        }
        else
        {
            MoveAndRotate();
        }
    }

    // Sets the enemy's position to the start of the spline
    public void SetSpawnOnSpline()
    {
        Vector3 localPosition = splineContainer.Spline.EvaluatePosition(0f);
        Vector3 worldPos = splineContainer.transform.TransformPoint(localPosition);
        transform.position = worldPos;
    }

    // Moves the enemy along the spline and rotates it to face the direction of travel
    public void MoveAndRotate()
    {
        Vector3 newPosition = splineContainer.Spline.EvaluatePosition(progress);
        Vector3 worldPosition = splineContainer.transform.TransformPoint(newPosition);
        Vector3 localTan = splineContainer.Spline.EvaluateTangent(progress);
        Vector3 worldTan = splineContainer.transform.TransformDirection(localTan);
        Quaternion worldRot = Quaternion.LookRotation(worldTan, Vector3.up);
        transform.position = worldPosition;
        transform.rotation = worldRot;
    }

    // Resets the speed to a default value
    public void ResetSpeed()
    {
        speed = speedCopy;
    }
}

using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class DefenderPathing : MonoBehaviour
{
    // Serialize field for spline path
    [SerializeField] private SplineContainer splineContainer;
    public float speed;
    private float progress;
    private float stopDistance = 0.1f;
    private Vector3 targetPos;
    private bool isAtTarget = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (splineContainer == null) { return; }
        float closestProgress = SplineUtilities.FindClosestProgressOnSpline(splineContainer.Spline, transform.position);
        Vector3 localPosition = splineContainer.Spline.EvaluatePosition(closestProgress);
        Vector3 worldPos = splineContainer.transform.TransformPoint(localPosition);
        targetPos = worldPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (splineContainer == null || isAtTarget) { return; }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= stopDistance)
        {
            isAtTarget = true;
        }
    }
}

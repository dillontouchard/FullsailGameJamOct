using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UnityEngine.Splines.Interpolators;
using System.Collections;

public class DefenderPathing : MonoBehaviour
{
    // Serialize field for spline path
    [SerializeField] private SplineContainer splineContainer;
    public float speed;
    public float rotSpeed;
    private float progress;
    private float stopDistance = 0.1f;
    private Vector3 targetPos;
    private Quaternion targetDir;
    private bool isAtTarget = false;
    private bool isRotating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (splineContainer == null) { return; }
        SetTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (splineContainer == null || isRotating) { return; }
        if (isAtTarget) 
        {
            if (!isRotating)
            {
                isRotating = true;
                StartCoroutine(RotateTowardsEnemy());
            }
            return;
        }
        MoveToClosestProgress();
    }

    public void SetTargetPosition()
    {
        float closestProgress = SplineUtilities.FindClosestProgressOnSpline(splineContainer.Spline, transform.position);
        Vector3 localPosition = splineContainer.Spline.EvaluatePosition(closestProgress);
        Vector3 worldPos = splineContainer.transform.TransformPoint(localPosition);
        targetPos = worldPos;
    }

    public void MoveToClosestProgress()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= stopDistance)
        {
            isAtTarget = true;
        }
    }

    public IEnumerator RotateTowardsEnemy()
    {
        bool isRotated = false;
        while (!isRotated)
        {
            if(Quaternion.Angle(transform.rotation, targetDir) <= 0.1)
            {
                isRotated = true;
            }
            Vector3 localTan = splineContainer.Spline.EvaluateTangent(progress);
            Vector3 worldTan = splineContainer.transform.TransformDirection(-localTan);
            targetDir = Quaternion.LookRotation(worldTan, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetDir, rotSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}

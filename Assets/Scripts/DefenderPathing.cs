using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UnityEngine.Splines.Interpolators;
using System.Collections;

public class DefenderPathing : MonoBehaviour
{
    // Spline path for use in pathing logic
    [SerializeField] private SplineContainer splineContainer;
    // Movement Speed and Rotation Speed
    public float speed;
    public float rotSpeed;
    // Progress along the spline
    private float progress;
    // Distance from target position to stop moving
    private float stopDistance = 0.1f;
    // Target Position on the spline
    private Vector3 targetPos;
    // Target Direction for rotation towards enemy
    private Quaternion targetDir;
    private bool isAtTarget = false;
    private bool isRotating = false;
    // Will be set by the spawner to determine which path to defend, will be used to clear the path when defender is destroyed
    [HideInInspector] public PathManager.LaneType type;
    [SerializeField] Animator defenderAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineContainer = PathManager.Instance.paths[(int)type];
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

    // Sets the target position to the closest point on the spline
    public void SetTargetPosition()
    {
        float closestProgress = SplineUtilities.FindClosestProgressOnSpline(splineContainer, transform.position);
        Vector3 localPosition = splineContainer.Spline.EvaluatePosition(closestProgress);
        Vector3 worldPos = splineContainer.transform.TransformPoint(localPosition);
        targetPos = worldPos;
    }

    // Moves the defender towards the target position
    public void MoveToClosestProgress()
    {
        defenderAnimator.SetBool("isWalking", true);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= stopDistance)
        {
            isAtTarget = true;
            defenderAnimator.SetBool("isWalking", false);
        }
    }

    // When at target position, rotate to face the opposite direction of the spline tangent (direction of incoming enemies)
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
            Vector3 worldTan = splineContainer.transform.TransformDirection(-localTan); // Negate to face opposite direction of enemy travel
            targetDir = Quaternion.LookRotation(worldTan, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetDir, rotSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}

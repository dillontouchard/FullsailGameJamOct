using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyPathing : MonoBehaviour
{
    // Serialize field for spline path
    [SerializeField] private SplineContainer splineContainer;
    public float speed;
    private float progress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 localPosition = splineContainer.Spline.EvaluatePosition(0f);
        Vector3 worldPos = splineContainer.transform.TransformPoint(localPosition);
        transform.position = worldPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (splineContainer == null) { return; }
        progress += speed * Time.deltaTime / splineContainer.Spline.GetLength();
        if (progress > 1f)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 newPosition = splineContainer.Spline.EvaluatePosition(progress);
            Vector3 worldPosition = splineContainer.transform.TransformPoint(newPosition);
            transform.position = worldPosition;
        }
    }
}

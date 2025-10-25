using UnityEngine;
using UnityEngine.Splines;

public static class SplineUtilities
{
    // Finds the closest progress value on the spline to the given point
    public static float FindClosestProgressOnSpline(SplineContainer spline, Vector3 point, float length = 1.0f)
    {
        float closestProgress = 0f;
        float closestDistanceSqr = float.MaxValue;
        for (float i = 0.0f; i <= length; i+=0.1f)
        {
            Vector3 localPosition = spline.Spline.EvaluatePosition(i);
            Vector3 worldSplinePoint = spline.transform.TransformPoint(localPosition);
            float distanceSqr = (worldSplinePoint - point).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestProgress = i;
            }
        }
        return closestProgress;
    }
}

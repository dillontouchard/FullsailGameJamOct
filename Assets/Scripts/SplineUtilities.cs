using UnityEngine;
using UnityEngine.Splines;

public static class SplineUtilities
{
    // Finds the closest progress (0 to 1) on the spline to the given point
    public static float FindClosestProgressOnSpline(Spline spline, Vector3 point, int sampleCount = 100)
    {
        float closestProgress = 0f;
        float closestDistanceSqr = float.MaxValue;
        for (int i = 0; i <= sampleCount; i++)
        {
            float t = (float)i / sampleCount;
            Vector3 splinePoint = spline.EvaluatePosition(t);
            float distanceSqr = (splinePoint - point).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestProgress = t;
            }
        }
        return closestProgress;
    }
}

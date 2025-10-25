using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

// Class to manage paths and track taken lanes for melee defenders
public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    public SplineContainer[] paths;
    public enum LaneType { Lane1, Lane2, Lane3};
    public List<LaneType> takenPaths = new();

    void Awake()
    {
        if(Instance != this)
        {
            Instance = this;
        }
    }
}

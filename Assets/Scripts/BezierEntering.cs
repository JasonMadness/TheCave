using System;
using BezierSolution;
using Unity.VisualScripting;
using UnityEngine;

public class BezierEntering : MonoBehaviour
{
    [SerializeField] private BezierSpline _spline;

    public BezierSpline GetSpline()
    {
        return _spline;
    }
}

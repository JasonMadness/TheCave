using System;
using BezierSolution;
using UnityEngine;

public class BezierMovement : MonoBehaviour
{
    [SerializeField] private SimpleFPSMovement _movement;
    [SerializeField] private BezierWalkerWithSpeed _walker;

    private void OnTriggerEnter(Collider other)
    {
        if (_movement._spline == null)
        {
            if (other.gameObject.TryGetComponent(out BezierEntering entering))
            {
                _walker.spline = entering.GetSpline();
                _movement.SetSpline(entering.GetSpline());
            }
        }
    }
}
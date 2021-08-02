using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUtilities : MonoBehaviour
{
    public LineRenderer lineRenderer;

    LineRenderer LR => (lineRenderer == null) ? (GetComponent<LineRenderer>()) : (lineRenderer);

    /// <summary>
    /// Checks if the closest distance to <paramref name="_point"/> is within the width of the line
    /// </summary>
    /// <param name="_point">The point to check</param>
    /// <returns>True if within the line width</returns>
    public bool IsOnLine(Vector3 _point)
    {
        return (GetDistanceToLine(_point) < (LR.startWidth / 2));
    }

    /// <summary>
    /// Gets the distance from <paramref name="_point"/> to the line
    /// </summary>
    /// <param name="_point">Point to get distance to</param>
    /// <returns>The distance to <paramref name="_point"/></returns>
    public float GetDistanceToLine(Vector3 _point)
    {
        Vector3[] points = new Vector3[LR.positionCount];
        LR.GetPositions(points);

        Vector2 point2D = new Vector2(_point.x, _point.z);

        float closestDist = Mathf.Infinity;
        Vector2 closestPoint = Vector2.zero;
        for (int i = 0; i < points.Length - 1; i++) //Avoid overflow with plus one
        {
            Vector2 pointA = new Vector2(points[i].x, points[i].z);
            Vector2 pointB = new Vector2(points[i + 1].x, points[i + 1].z);

            Vector2 point = FindNearestPointOnLine(pointA, pointB, point2D);

            float dist = Vector2.Distance(point2D, point);
            if (dist < closestDist) //if closer
            {
                closestDist = dist;
                closestPoint = point;
            }
        }

        return closestDist;
    }

    /// <summary>
    /// Returns the closes point on the line render to <paramref name="_point"/>
    /// </summary>
    /// <param name="_point">The point to get closest to</param>
    /// <returns>Returns the closest point on line, with a flattened Y value</returns>
    public Vector3 GetClosestPointOnLine(Vector3 _point)
    {
        Vector3[] points = new Vector3[LR.positionCount];
        LR.GetPositions(points);

        Vector2 point2D = new Vector2(_point.x, _point.z);

        float closestDist = Mathf.Infinity;
        Vector2 closestPoint = Vector2.zero;
        for (int i = 0; i < points.Length - 1; i++) //Avoid overflow with plus one
        {
            Vector2 pointA = new Vector2(points[i].x, points[i].z);
            Vector2 pointB = new Vector2(points[i + 1].x, points[i + 1].z);

            Vector2 point = FindNearestPointOnLine(pointA, pointB, point2D);

            float dist = Vector2.Distance(point2D, point);
            if (dist < closestDist) //if closer
            {
                closestDist = dist;
                closestPoint = point;
            }
        }

        return new Vector3(closestPoint.x, 0.0f, closestPoint.y);
    }

    Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 end, Vector2 point)
    {
        //Get heading
        Vector2 heading = (end - origin);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        //Do projection from the point but clamp it
        Vector2 lhs = point - origin;
        float dotP = Vector2.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return origin + heading * dotP;
    }


}

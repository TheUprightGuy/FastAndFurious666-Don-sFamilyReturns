using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUtilities : MonoBehaviour
{
    #region Singleton
    public static RoadUtilities instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one RoadUtils Exists");
            Destroy(this.gameObject);
        }
        instance = this;
    }
    #endregion Singleton;

    public void SetRoad(LineRenderer _lr)
    {
        lineRenderer = _lr;
    }

    public LineRenderer lineRenderer;

    //[HideInInspector]
    //public LineRenderer lineRenderer => (lineRenderer == null) ? (GetComponent<LineRenderer>()) : (lineRenderer);

    public float LineWidth => lineRenderer.startWidth / 2.0f;
    /// <summary>
    /// Gets the total world length of the line
    /// </summary>
    /// <returns>The length of the line</returns>
    public float GetLengthOfLine()
    {
        float retLength = 0.0f;
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        for (int i = 0; i < points.Length - 1; i++)
        {
            retLength += Vector3.Distance(points[i], points[i + 1]);
        }

        return (retLength);
    }

    /// <summary>
    /// Gets points at <paramref name="_percentageAlongLine"/> percent along line
    /// </summary>
    /// <param name="_percentageAlongLine">The percentage along line, from 0.0f to 1.0f</param>
    /// <returns>The points along the line, Vector3.zero will be returned with an invalid percentage</returns>
    public Vector3 GetPointAlongLine(float _percentageAlongLine)
    {
        float distanceAim = GetLengthOfLine() * _percentageAlongLine;

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        float retLength = 0.0f;
        for (int i = 0; i < points.Length - 1; i++)
        {
            float newLength = retLength + Vector3.Distance(points[i], points[i + 1]);

            if (newLength >= distanceAim) //Then the desired point is between these two points
            {
                float amountAlongLine = distanceAim - newLength;
                Vector3 dir = (points[i + 1] - points[i]).normalized;
                return points[i] + (dir * amountAlongLine);
            }
            else
            {
                retLength = newLength;
            }
            
        }

        return (Vector3.zero);
    }

    /// <summary>
    /// Gets points at <paramref name="_percentageAlongLine"/> percent along line
    /// </summary>
    /// <param name="_percentageAlongLine">The percentage along line, from 0.0f to 1.0f</param>
    /// <returns>The points along the line, Vector3.zero will be returned with an invalid percentage</returns>
    public void GetPointAlongLine(float _percentageAlongLine, out Vector3 direction, out Vector3 point)
    {
        float distanceAim = GetLengthOfLine() * _percentageAlongLine;

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        float retLength = 0.0f;
        for (int i = 0; i < points.Length - 1; i++)
        {
            float newLength = retLength + Vector3.Distance(points[i], points[i + 1]);

            if (newLength >= distanceAim) //Then the desired point is between these two points
            {
                float amountAlongLine = distanceAim - newLength;
                Vector3 dir = (points[i + 1] - points[i]).normalized;
                point = points[i] + (dir * amountAlongLine);
                direction = dir;
                return ;
            }
            else
            {
                retLength = newLength;
            }

        }


        point = Vector3.zero;
        direction = Vector3.zero;
    }

    /// <summary>
    /// Gets the points of segments with a greater angle than <paramref name="_angle"/>
    /// </summary>
    /// <param name="_angle">The comparison angle</param>
    /// <returns>A list of points</returns>
    public List<Vector3> GetPointsAtAngle(float _angle)
    {
        List<Vector3> retList = new List<Vector3>();

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        for (int i = 1; i < points.Length - 1; i++)
        {
            Vector3 dirA = (points[i - 1] - points[i]).normalized;
            Vector3 dirB = (points[i + 1] - points[i]).normalized;

            float angleBetween = 180.0f - Vector3.Angle(dirA, dirB);

            if (angleBetween > _angle)
            {
                retList.Add(points[i]);
            }
        }
        return retList;
    }

    /// <summary>
    /// Gets the indexes of segments with a greater angle than <paramref name="_angle"/>
    /// </summary>
    /// <param name="_angle">The comparison angle</param>
    /// <returns>A list of indexes</returns>
    public List<int> GetIndexesAtAngle(float _angle)
    {
        List<int> retList = new List<int>();

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        for (int i = 1; i < points.Length - 1; i++)
        {
            Vector3 dirA = (points[i - 1] - points[i]).normalized;
            Vector3 dirB = (points[i + 1] - points[i]).normalized;

            float angleBetween = 180.0f - Vector3.Angle(dirA, dirB);

            if (angleBetween > _angle)
            {
                retList.Add(i);
            }
        }
        return retList;
    }
    /// <summary>
    /// Checks if the closest distance to <paramref name="_point"/> is within the width of the line
    /// </summary>
    /// <param name="_point">The point to check</param>
    /// <returns>True if within the line width</returns>
    public bool IsOnLine(Vector3 _point)
    {
        return (GetDistanceToLine(_point) < (lineRenderer.startWidth / 2));
    }

    /// <summary>
    /// Gets the distance from <paramref name="_point"/> to the line
    /// </summary>
    /// <param name="_point">Point to get distance to</param>
    /// <returns>The distance to <paramref name="_point"/></returns>
    public float GetDistanceToLine(Vector3 _point)
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

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


    public Vector3 GetPointAheadOnTrack(Vector3 _point, float distAhead)
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        Vector2 point2D = new Vector2(_point.x, _point.z);

        float closestDist = Mathf.Infinity;
        Vector2 closestPoint = Vector2.zero;
        int lastIndexPoint = 0;
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
                lastIndexPoint = i;
            }
        }

        Vector3 retPoint = Vector3.zero;
        float amountOfDistToIncrease = distAhead;
        for (int i = lastIndexPoint; i < points.Length - 1; i++)
        {
            float nextDist = Vector3.Distance(points[i], points[i + 1]);

            if ((amountOfDistToIncrease - nextDist) <= 0)//If now within the distances
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;
                retPoint = points[i] + (dir * amountOfDistToIncrease);
                break;
            }
            else
            {
                amountOfDistToIncrease -= nextDist;
            }
        }
        return retPoint;
    }

    /// <summary>
    /// Returns the closes point on the line render to <paramref name="_point"/>
    /// </summary>
    /// <param name="_point">The point to get closest to</param>
    /// <returns>Returns the closest point on line, with a flattened Y value</returns>
    public Vector3 GetClosestPointOnLine(Vector3 _point)
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

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

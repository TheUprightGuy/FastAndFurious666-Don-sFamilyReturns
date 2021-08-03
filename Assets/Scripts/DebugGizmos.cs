using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float dist;
    public float angle = 40.0f;
    public RoadUtilities RoadUtils;
    private void OnDrawGizmos()
    {
        if (RoadUtils == null)
        {
            return;
        }

        Vector3 wat = RoadUtils.GetClosestPointOnLine(transform.position);
        wat.y = transform.position.y;
        Gizmos.DrawSphere(wat, 1.0f);
        Gizmos.DrawLine(transform.position, wat);
        
        Vector3 closestPoint = RoadUtils.GetClosestPointOnLine(transform.position);
        Vector3 refPoint = transform.position;
        refPoint.y = 0.0f;

        Vector3 dir = refPoint - closestPoint;

        Gizmos.DrawSphere(transform.position - dir.normalized , 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(RoadUtils.GetPointAlongLine(dist), 1.0f);

        List<Vector3> pointList = RoadUtils.GetPointsAtAngle(angle);

        Gizmos.color = Color.green;
        foreach (Vector3 item in pointList)
        {
            Gizmos.DrawSphere(item, 0.5f);
        }
    }
}

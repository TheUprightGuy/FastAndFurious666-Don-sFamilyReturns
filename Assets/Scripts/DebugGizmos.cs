using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
    float dist;
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
        dist = RoadUtils.GetDistanceToLine(transform.position);

        Vector3 closestPoint = RoadUtils.GetClosestPointOnLine(transform.position);
        Vector3 refPoint = transform.position;
        refPoint.y = 0.0f;

        Vector3 dir = refPoint - closestPoint;

        Gizmos.DrawSphere(transform.position - dir.normalized , 0.5f);
    }
}

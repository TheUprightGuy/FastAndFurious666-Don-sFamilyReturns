using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Dependencies")]
    public Transform pointsParent = null;
    public Bezier_Spline RoadSpline = null;

    public Transform EndPoint = null;
    [Header("Sizing")]
    public int SegmentCount = 20;
    public float DistanceBetweenSegments = 20.0f;

    [Header("Randomiser")]
    public float XChangeMin = 0.0f;
    public float XChangeMax = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        BuildRoad();
        
        if (RoadSpline != null)
        {
            RoadSpline.CreateSpline();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildRoad()
    {
        if (pointsParent == null)
        {
            pointsParent = this.transform;
        }

        if (pointsParent.childCount <= 0)
        {
            return;
        }

        Vector3 lastPoint = pointsParent.GetChild(pointsParent.childCount - 1).transform.localPosition;

        for (int i = 0; i < SegmentCount; i++)
        {
            lastPoint.y += DistanceBetweenSegments;

            float rangeToMove = Random.Range(XChangeMin, XChangeMax); //Get the amount to change by
            rangeToMove *= (Random.Range(0.0f, 1.0f) > 0.5f) ? (1) : (-1); //Coin flip if left or right movement

            lastPoint.x += rangeToMove;

            GameObject newObj = new GameObject();
            newObj.name = i.ToString();
            newObj.transform.parent = pointsParent;
            newObj.transform.localPosition = lastPoint;
        }

        if (EndPoint != null)
        {
            EndPoint.position = pointsParent.GetChild(pointsParent.childCount - 1).transform.position;
        }
    }    
}

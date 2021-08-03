using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Dependencies")]
    public Transform pointsParent = null;
    public Bezier_Spline RoadSpline = null;
    public RoadUtilities RoadUtils = null;
    public Transform EndPoint = null;

    [Header("Prefabs")]
    public GameObject tireStack;

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

        if (RoadUtils != null)
        {

            AddAccesories();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddAccesories()
    {
        List<int> indexList = RoadUtils.GetIndexesAtAngle(3.0f); //Completely arbitrary number lmao

        Vector3[] points = new Vector3[RoadUtils.LR.positionCount];
        RoadUtils.LR.GetPositions(points);

        foreach (int i in indexList)
        {
            Vector3 dir = (points[i - 1] - points[i]).normalized;
            Vector3 left = Vector3.Cross(dir, Vector3.up).normalized;
            Vector3 right = -left;

            float distToEdge = 4.0f;
            GameObject a = Instantiate(tireStack, points[i] + (left * distToEdge), Quaternion.identity, this.transform);
            a.transform.forward = left;
            a = Instantiate(tireStack, points[i] + (right * distToEdge), Quaternion.identity, this.transform);
            a.transform.forward = right;
        }
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

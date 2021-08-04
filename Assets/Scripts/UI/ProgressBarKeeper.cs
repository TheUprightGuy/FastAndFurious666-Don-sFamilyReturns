using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarKeeper : MonoBehaviour
{
    [Header("Dependencies")]
    public Transform startPoint;
    public Transform endPoint;
    public Transform TrackPoint;

    RectTransform thisRect;
    [Header("Settings")]
    public int NumOfIncrements = 60;
    public float percentDone;

    
    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBar();
    }

    void SetBar()
    {
        float distanceTotal = Vector3.Distance(startPoint.position, endPoint.position);
        float currentDistance = Vector3.Distance(endPoint.position, TrackPoint.position);

        percentDone = 1.0f - currentDistance / distanceTotal;

        int incrementsDone = Mathf.RoundToInt(NumOfIncrements * percentDone);

        float totalScale = incrementsDone * (1.0f / NumOfIncrements);

        Vector3 newScale = thisRect.localScale;
        newScale.x = totalScale;
        thisRect.localScale = newScale;

    }
}

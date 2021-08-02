using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoadPointer : MonoBehaviour
{
    public Camera WorldCam;
    public Transform ReferencePoint;
    public RoadUtilities RoadUtils;
    public RectTransform RenderTexture;
    Image arrowImage = null;
    RectTransform rect = null;

    Vector3 savedPos;
    // Start is called before the first frame update
    void Start()
    {
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
        }
        savedPos = rect.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SetArrow();
    }

    Vector3 a;
    Vector3 b;
    void SetArrow()
    {
        if (arrowImage == null)
        {
            arrowImage = GetComponent<Image>();
        }
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
        }
        if (RoadUtils == null || ReferencePoint == null || WorldCam == null)
        {
            return;
        }

        //pointOfClosest = WorldCam.WorldToScreenPoint(ReferencePoint.position);

        /*Left*/
        float left = RenderTexture.offsetMin.x;
        /*Right*/
        float Right = RenderTexture.offsetMax.x;
        /*Top*/
        float Top = RenderTexture.offsetMax.y;
        /*Bottom*/
        float Bottom = RenderTexture.offsetMin.y;

        Vector3 newPos = RenderTexture.position;
        newPos.x += left + 50.0f;
        newPos.y += Bottom;
        rect.position = newPos;
    }
}

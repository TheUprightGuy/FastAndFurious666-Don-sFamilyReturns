using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWidth : MonoBehaviour
{

    public float StartWidth = 10.0f;
    public float EndWidth = 10.0f;

    LineRenderer thisLine = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (thisLine == null)
        {
            thisLine = GetComponent<LineRenderer>();
        }

        thisLine.startWidth = StartWidth;
        thisLine.endWidth = EndWidth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

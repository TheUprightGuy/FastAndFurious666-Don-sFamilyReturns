using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlAI : MonoBehaviour
{
    public RoadUtilities RoadUtils;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (RoadUtils !=null)
        {
            Vector3 pos = RoadUtils.GetPointAheadOnTrack(transform.position, 10.0f);


            Gizmos.DrawSphere(pos, 0.5f);
        }
    }
}

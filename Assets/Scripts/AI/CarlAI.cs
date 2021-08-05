using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlAI : MonoBehaviour
{
    [Header("Dependencies")]
    public RoadUtilities RoadUtils;

    [Header("Forces")]
    public float BrakeSpeed = 50.0f;
    public float MoveAcceleration = 10.0f;
    public float MaxSpeed = 30.0f;

    [Header("AI")]
    public float PredictionDistance = 5.0f;
    public float lookAheadDist = 7.0f;
    public float trackWidthMulti = 0.5f;
    public LayerMask AvoidingLayers;
    
    Rigidbody rigidBody = null;
    bool freeze = true;

    // Aggro
    bool hostile;
    Transform player;

    public void ToggleHostile(bool _toggle, Transform _player)
    {
        hostile = _toggle;
        player = _player;
    }

    public void ToggleFreeze(bool _toggle)
    {
        freeze = _toggle;
    }

    // Start is called before the first frame update
    void Start()
    {
        CallbackHandler.instance.toggleFreeze += ToggleFreeze;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.toggleFreeze -= ToggleFreeze;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (freeze)
        {
            return;
        }
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }


        Vector3 projectedPos = transform.position + (rigidBody.velocity.normalized * PredictionDistance);
        float distToTrack = RoadUtils.GetDistanceToLine(projectedPos);

        bool isHeadingForDisaster = 
            distToTrack > RoadUtils.LineWidth * trackWidthMulti ||
            Physics.Raycast(transform.position, rigidBody.velocity.normalized, PredictionDistance, AvoidingLayers.value);




        if (isHeadingForDisaster)
        {
            Vector3 velocityDir = rigidBody.velocity.normalized;
            rigidBody.AddForce((-velocityDir) * BrakeSpeed);
        }

        if (rigidBody.velocity.magnitude < MaxSpeed)
        {
            Vector3 moveDir = transform.forward;
            if (isHeadingForDisaster)
            {
                Vector3 pos = RoadUtils.GetPointAheadOnTrack(transform.position, PredictionDistance);

                Vector3 dirToPredict = pos - transform.position;
                moveDir += dirToPredict;
            }

            moveDir = moveDir.normalized;
            rigidBody.AddForce(moveDir * MoveAcceleration);
        }

        transform.forward = rigidBody.velocity.normalized;
    }
    Color GizmoColor = Color.blue;
    private void OnDrawGizmos()
    {
        //if (RoadUtils !=null)
        //{
        //    Gizmos.color = GizmoColor;
        //    Vector3 pos = RoadUtils.GetPointAheadOnTrack(transform.position, PredictionDistance);
        //    Gizmos.DrawSphere(pos, 0.25f);
        //    GizmoColor = Color.blue;

        //    Vector3 projectedPos = transform.position + (rigidBody.velocity.normalized * PredictionDistance);
        //    Gizmos.DrawCube(projectedPos, Vector3.one * 0.5f);

        //}
    }
}

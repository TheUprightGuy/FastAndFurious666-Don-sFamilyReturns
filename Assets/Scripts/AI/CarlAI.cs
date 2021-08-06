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
    public Transform player;

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
        RoadUtils = RoadUtilities.instance;
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

    void OhNoBigBroImStuck()
    {
        //transform.position = transform.position + (Vector3.up * 1.0f);
        //transform.up = Vector3.up;
        rigidBody.MoveRotation(Quaternion.LookRotation(Vector3.forward, Vector3.up));
    }
    void ApplyMovement()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        bool isHeadingForDisaster = false;
        if (RoadUtils != null)
        {
            Vector3 projectedPos = transform.position + (rigidBody.velocity.normalized * PredictionDistance);
            float distToTrack = RoadUtils.GetDistanceToLine(projectedPos);
            isHeadingForDisaster =
            distToTrack > RoadUtils.LineWidth * trackWidthMulti ||
            Physics.Raycast(transform.position, rigidBody.velocity.normalized, PredictionDistance, AvoidingLayers.value);
        }
        else
        {
            isHeadingForDisaster = 
                Physics.Raycast(transform.position, rigidBody.velocity.normalized, PredictionDistance, AvoidingLayers.value);
        }
        

        if (Vector3.Angle(transform.up, Vector3.up) >= 90.0f) //On side
        {
            OhNoBigBroImStuck();
        }

        if (isHeadingForDisaster)
        {
            Vector3 velocityDir = rigidBody.velocity.normalized;
            velocityDir = Vector3.ProjectOnPlane(velocityDir, Vector3.up);
            rigidBody.AddForce((-velocityDir) * BrakeSpeed);
        }

        if (rigidBody.velocity.magnitude < MaxSpeed && //Not above speed limit
            Physics.Raycast(transform.position, -transform.up, 1.0f/*, AvoidingLayers.value*/)) //On the ground
        {
            Vector3 moveDir = Vector3.zero;
            if (RoadUtils == null && player != null) //Fucking attack lads
            {
                moveDir = (player.transform.position - transform.position).normalized;
            }
            else
            {
                moveDir = transform.forward;
                if (isHeadingForDisaster)
                {
                    Vector3 pos = RoadUtils.GetPointAheadOnTrack(transform.position, PredictionDistance);

                    Vector3 dirToPredict = pos - transform.position;
                    moveDir += dirToPredict;
                }
                else if (hostile)//Will only be hostile if not crashing
                {
                    Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;

                    if (Vector3.Angle(dirToPlayer, transform.forward) < 90.0f) //If not in front ignore
                    {
                        moveDir += dirToPlayer;
                    }
                }
            }



            moveDir = moveDir.normalized;
            moveDir = Vector3.ProjectOnPlane(moveDir, Vector3.up);
            rigidBody.AddForce(moveDir * MoveAcceleration);

            rigidBody.MoveRotation(Quaternion.LookRotation(rigidBody.velocity.normalized, Vector3.up));
            //transform.forward = rigidBody.velocity.normalized;
        }

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

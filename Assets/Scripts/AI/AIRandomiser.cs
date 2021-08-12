using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandomiser : MonoBehaviour
{
    public float MaxSpeedMin = 30.0f;
    public float MaxSpeedMax = 30.0f;
    [Space]
    public float AccelerationMin = 20.0f;
    public float AccelerationMax = 30.0f;
    [Space]
    public float BrakeSpeedMin = 20.0f;
    public float BrakeSpeedMax = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        RandomiseChildren();
    }

    void RandomiseChildren()
    {
        foreach (CarlAI ai in transform.GetComponentsInChildren<CarlAI>())
        {
            ai.BrakeSpeed = Random.Range(BrakeSpeedMin, BrakeSpeedMax);
            ai.MoveAcceleration = Random.Range(AccelerationMin, AccelerationMax);
            ai.MaxSpeed = Random.Range(MaxSpeedMin, MaxSpeedMax);
        }
    }
}

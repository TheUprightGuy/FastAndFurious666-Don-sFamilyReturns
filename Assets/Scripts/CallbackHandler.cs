using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackHandler : MonoBehaviour
{
    #region Singleton Setup
    public static CallbackHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Callback Handler exists!");
            Destroy(this.transform.root.gameObject);
            return;
        }
        instance = this;
    }
    #endregion Singleton Setup

    #region UICallbacks
    public Action<float, float> updateSpeedometer;
    public void UpdateSpeedometer(float _speed, float _maxSpeed)
    {
        if (updateSpeedometer != null)
            updateSpeedometer(_speed, _maxSpeed);
    }

    public Action<float, float> updateProgress;
    public void UpdateProgress(float _distance, float _maxDistance)
    {
        if (updateProgress != null)
            updateProgress(_distance, _maxDistance);
    }
    #endregion UICallbacks
}

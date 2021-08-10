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

    private void Start()
    {
        Invoke("Setup", 0.1f);
    }

    void Setup()
    {
        ToggleRocket(false);
    }

    bool killed = false;
    public void HasKilled()
    {
        killed = true;
    }
    public bool GetKilled()
    {
        return killed;
    }

    public GameObject AIContainer;
    public bool CheckSurvivors()
    {
        if (AIContainer.transform.childCount > 1)
            return true;

        return false;
    }

    #region UICallbacks
    public Action<float, float> updateSpeedometer;
    public void UpdateSpeedometer(float _speed, float _maxSpeed)
    {
        if (updateSpeedometer != null)
            updateSpeedometer(_speed, _maxSpeed);
    }
    public Action<float, float> updateAmmo;
    public void UpdateAmmo(float _ammo, float _maxAmmo)
    {
        if (updateAmmo != null)
            updateAmmo(_ammo, _maxAmmo);
    }

    public Action<float> updateHealth;
    public void UpdateHealth(float _perc)
    {
        if (updateHealth != null)
            updateHealth(_perc);
    }

    public Action<float, float> updateProgress;
    public void UpdateProgress(float _distance, float _maxDistance)
    {
        if (updateProgress != null)
            updateProgress(_distance, _maxDistance);
    }

    public Action<EndState> showEndScreen;
    public void ShowEndScreen(EndState _state)
    {
        if (showEndScreen != null)
            showEndScreen(_state);
    }

    public Action<bool> toggleHint;
    public void ToggleHint(bool _toggle)
    {
        if (toggleHint != null)
            toggleHint(_toggle);
    }


    public Action<bool> toggleRocket;
    public void ToggleRocket(bool _toggle)
    {
        if (toggleRocket != null)
            toggleRocket(_toggle);
    }
    #endregion UICallbacks

    public Action<Transform> setRocketTarget;
    public void SetRocketTarget(Transform _target)
    {
        if (setRocketTarget != null)
            setRocketTarget(_target);
    }

    public Action<bool> toggleFreeze;
    public void ToggleFreeze(bool _toggle)
    {
        if (toggleFreeze != null)
            toggleFreeze(_toggle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    #region Setup
    [Header("Setup Requirements")]
    public UnityEngine.UI.Image speedometer;

    #endregion Setup
    #region Callbacks
    void Start()
    {
        CallbackHandler.instance.updateSpeedometer += UpdateSpeedometer;    
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateSpeedometer -= UpdateSpeedometer;
    }
    #endregion Callbacks

    public void UpdateSpeedometer(float _speed, float _maxSpeed)
    {
        speedometer.fillAmount = _speed / _maxSpeed;
    }
}

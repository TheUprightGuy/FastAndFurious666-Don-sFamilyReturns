using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    #region Setup
    // Local Variables
    TMPro.TextMeshProUGUI text;
    UnityEngine.UI.Image speedometer;
    // Get Local Variables
    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        speedometer = GetComponentInChildren<UnityEngine.UI.Image>();
    }
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
        text.SetText(Mathf.RoundToInt(_speed * 2.5f).ToString() + "MPH");
        speedometer.fillAmount = _speed / _maxSpeed;
    }
}

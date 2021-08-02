using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    UnityEngine.UI.Image speedometer;
    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        speedometer = GetComponentInChildren<UnityEngine.UI.Image>();
    }

    void Start()
    {
        CallbackHandler.instance.updateSpeedometer += UpdateSpeedometer;    
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.updateSpeedometer -= UpdateSpeedometer;
    }



    public void UpdateSpeedometer(float _speed, float _maxSpeed)
    {
        text.SetText(Mathf.RoundToInt(_speed * 2.5f).ToString() + "MPH");
        speedometer.fillAmount = _speed / _maxSpeed;
    }
}

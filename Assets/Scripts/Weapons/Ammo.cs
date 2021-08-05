using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    #region Setup
    [Header("Setup Requirements")]
    public UnityEngine.UI.Image ammo;

    #endregion Setup
    #region Callbacks
    void Start()
    {
        CallbackHandler.instance.updateAmmo += UpdateAmmo;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateAmmo -= UpdateAmmo;
    }
    #endregion Callbacks

    public void UpdateAmmo(float _speed, float _maxSpeed)
    {
        ammo.fillAmount = _speed / _maxSpeed;
    }
}

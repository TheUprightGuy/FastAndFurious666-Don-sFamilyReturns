using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTargeting : MonoBehaviour
{
    [Header("Setup Requirements")]
    public LockOnUI uiElement;

    // Local Variable
    [HideInInspector] public List<LockOnTarget> targets = new List<LockOnTarget>();

    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.toggleRocket += ToggleRocket;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.toggleRocket -= ToggleRocket;
    }
    #endregion Callbacks
    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        LockOnTarget temp = other.GetComponent<LockOnTarget>();

        if (temp)
        {
            targets.Add(temp);
            uiElement.SetTarget(temp);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        LockOnTarget temp = other.GetComponent<LockOnTarget>();

        if (temp)
        {
            targets.Remove(temp);
            uiElement.SetTarget(null);
        }
    }
    #endregion Triggers

    public void ToggleRocket(bool _toggle)
    {
        this.enabled = _toggle;
    }
}

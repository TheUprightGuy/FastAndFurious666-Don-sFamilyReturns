using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnUI : MonoBehaviour
{
    [Header("Setup Variables")]
    public TMPro.TextMeshProUGUI text;

    // Local Variables
    public LockOnTarget target;
    RectTransform rect;
    Image image;
    Vector3 tracking;
    public int lockOn = 0;

    #region Setup
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.enabled = false;

        TurnOffText();
    }
    #endregion Setup
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

    public void ToggleRocket(bool _toggle)
    {
        this.enabled = _toggle;
    }


    // Update is called once per frame
    void Update()
    {
        image.enabled = target;

        if (target)
        {
            tracking = Camera.main.WorldToViewportPoint(target.transform.position);
            tracking.x *= Screen.width;
            tracking.y *= Screen.height;

            rect.position = tracking;

            if (lockOn >= 6.0f)
            {
                text.SetText("LOCK");
                StopAllCoroutines();
                TurnOnText();
                CallbackHandler.instance.SetRocketTarget(target.transform);
                return;
            }
            CallbackHandler.instance.SetRocketTarget(null);
            return;
        }
        CallbackHandler.instance.SetRocketTarget(null);
    }

    public void SetTarget(LockOnTarget _target)
    {
        if (_target != null && this.enabled)
        {
            target = _target;
            text.SetText("LOCKING ON");
            lockOn = 0;
            StartCoroutine(FlashLockOn());
            return;
        }

        target = null;
        TurnOffText();
        StopAllCoroutines();
    }

    IEnumerator FlashLockOn()
    {
        while (true)
        {
            text.alpha = (text.alpha == 1.0f) ? 0.0f : 1.0f;
            lockOn++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    #region Utility
    void TurnOffText()
    {
        text.alpha = 0.0f;
    }
    void TurnOnText()
    {
        text.alpha = 1.0f;
    }
    #endregion Utility
}

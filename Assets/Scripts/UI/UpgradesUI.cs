using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    #region Setup
    Image image;
    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.toggleHint += ToggleHint;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.toggleHint -= ToggleHint;
    }
    #endregion Callbacks

    public void ToggleHint(bool _toggle)
    {
        if (_toggle)
        {
            StartCoroutine(FlashUpgradeHint());
            return;
        }
        StopAllCoroutines();
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    IEnumerator FlashUpgradeHint()
    {
        while (true)
        {
            image.color = (image.color.a == 1.0f) ? new Color(1.0f, 1.0f, 1.0f, 0.0f) : Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
}

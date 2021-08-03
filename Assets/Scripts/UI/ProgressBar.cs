using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Setup
    // Local Variables
    Image progress;
    float width;
    // Get Local Components
    private void Awake()
    {
        width = GetComponent<RectTransform>().rect.width;
        progress = GetComponentsInChildren<Image>()[1];
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.updateProgress += UpdateProgress;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateProgress -= UpdateProgress;
    }
    #endregion Callbacks
    #region Utility
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    #endregion Utility

    public void UpdateProgress(float _distance, float _maxDistance)
    {
        float pos = Mathf.Clamp(map(_distance, _maxDistance, 0, -width / 2, width / 2), -width / 2, width / 2);
        progress.GetComponent<RectTransform>().localPosition = new Vector3(pos, 0.0f, 0.0f);
    }
}

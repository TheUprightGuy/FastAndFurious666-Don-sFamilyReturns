using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public int segments;
    public float perc;

    #region Setup
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    #endregion
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.updateHealth += SetHealth;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateHealth -= SetHealth;
    }
    #endregion Callbacks

    public void SetHealth(float _perc)
    {
        int incrementsDone = Mathf.RoundToInt(segments * _perc);

        image.fillAmount = (1.0f / segments) * incrementsDone;
    }

}

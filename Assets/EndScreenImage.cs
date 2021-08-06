using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EndState
{
    First = 0,
    SecondThird,
    Last,
    Killed
}

public class EndScreenImage : MonoBehaviour
{
    #region Setup
    private void Awake()
    {
        image.enabled = false;
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.showEndScreen += ShowEndScreen;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.showEndScreen -= ShowEndScreen;
    }
    #endregion Callbacks

    [Header("Setup Requirements")]
    public List<Sprite> endScreens;
    public Image image;


    public void ShowEndScreen(EndState _end)
    {
        image.enabled = true;
        image.sprite = endScreens[(int)_end];
    }
}

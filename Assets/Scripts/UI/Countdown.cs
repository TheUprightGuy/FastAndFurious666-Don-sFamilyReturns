using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float timer;
    int index = -1;
    List<Image> countdown = new List<Image>();
    private void Awake()
    {
        foreach(Image n in this.GetComponentsInChildren<Image>())
        {
            countdown.Add(n);
        }
    }

    void Show(int _count)
    {
        for(int i = 0; i < countdown.Count; i++)
        {
            countdown[i].enabled = (i == _count);

            if (i == _count)
            {
                if (index != i)
                {
                    index = i;
                    AudioHandler.instance.PlayAudio(countdown[i].gameObject.name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int temp = Mathf.FloorToInt(timer);

        Show(temp);

        if (temp >= countdown.Count - 1)
        {
            CallbackHandler.instance.ToggleFreeze(false);
            Invoke("HideGo", 1.0f);
        }
    }

    void HideGo()
    {
        Show(-1);
        this.enabled = false;
    }
}

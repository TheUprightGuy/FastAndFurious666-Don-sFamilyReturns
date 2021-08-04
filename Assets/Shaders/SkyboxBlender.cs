using UnityEngine;
using System.Collections;

public class SkyboxBlender : MonoBehaviour
{
    public Material material;
    float timeOfDay = 0.0f;
    public float blendSpeed = 1.0f;


    public Color outputColor;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color lightBlend;

    public Light[] lights;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        timeOfDay = 0.5f;
    }
    
    void Update()
    {

        timeOfDay += Time.deltaTime * blendSpeed;
        if (timeOfDay >= 1.5f)
        {
            timeOfDay = 0.0f;
        }

        material.SetFloat("_Vec1TimeOfDay", timeOfDay);
        SetOutputColor();
        SetLightColor();
    }

    public void SetTimeOfDay(float _time)
    {
        timeOfDay = _time;
    }

    void SetOutputColor()
    {
        if (timeOfDay < 0.5f)
        {
            outputColor = Color.Lerp(color1, color2, timeOfDay * 2.0f);
        }
        else if (timeOfDay < 1.0f)
        {
            outputColor = Color.Lerp(color2, color3, (timeOfDay - 0.5f) * 2.0f);
        }
        else if (timeOfDay < 1.5f)
        {
            outputColor = Color.Lerp(color3, color1, (timeOfDay - 1.0f) * 2.0f);
        }
    }
    void SetLightColor()
    {
        foreach (Light n in lights)
        {
            n.color = Color.Lerp(outputColor, lightBlend, 0.8f);
        }
    }
}


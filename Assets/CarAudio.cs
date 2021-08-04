using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource engineAudio;
    public AudioSource skidAudio;
    public AudioSource crashAudio;

    public float speed;
    public bool skidding;

    public List<AudioClip> engineLoops;

    public void UpdateEngine(float _speed)
    {
        speed = _speed;
        if (_speed < 0.25f)
        {
            if (engineAudio.clip != engineLoops[0])
            {
                engineAudio.clip = engineLoops[0];
                engineAudio.Stop();
                engineAudio.Play();
            }
        }
        else if (_speed < 0.5f)
        {
            if (engineAudio.clip != engineLoops[1])
            {
                engineAudio.clip = engineLoops[1];
                engineAudio.Stop();
                engineAudio.Play();
            }
        }
        else if (_speed < 0.75f)
        {
            if (engineAudio.clip != engineLoops[2])
            {
                engineAudio.clip = engineLoops[2];
                engineAudio.Stop();
                engineAudio.Play();
            }
        }
        else
        {
            if (engineAudio.clip != engineLoops[3])
            {
                engineAudio.clip = engineLoops[3];
                engineAudio.Stop();
                engineAudio.Play();
            }
        }
    }

    public void SetSkidding(bool _toggle)
    {
        skidAudio.volume = _toggle ? 0.5f : 0.0f;
    }

    public void PlayCrashAudio(float _speed)
    {
        //if (!crashAudio.isPlaying)
        {
            crashAudio.volume = _speed;
            crashAudio.PlayOneShot(crashAudio.clip);
        }
    }
}

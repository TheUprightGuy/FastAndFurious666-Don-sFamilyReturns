using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {
        foreach(ParticleSystem n in GetComponentsInChildren<ParticleSystem>())
        {
            particles.Add(n);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ToggleParticles(true);
            return;
        }
        ToggleParticles(false);
    }

    void ToggleParticles(bool _toggle)
    {
        foreach(ParticleSystem n in particles)
        {
            if (_toggle)
            {
                if (!n.isPlaying)
                    n.Play();
            }
            else
            {
                n.Stop();
            }
        }
    }
}

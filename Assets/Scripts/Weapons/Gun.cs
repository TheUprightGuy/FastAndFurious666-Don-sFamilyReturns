using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    MachineGun,
    FlameThrower,
    RocketLauncher,
    None
}


public class Gun : MonoBehaviour
{
    List<ParticleSystem> particles = new List<ParticleSystem>();

    [Header("Weapon Attributes")]
    public GunType type;
    // public float damage
    // public int ammo
    // public int maxAmmo

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

    public void ToggleWeapon(GunType _type)
    {
        this.gameObject.SetActive(_type == type);
    }
}

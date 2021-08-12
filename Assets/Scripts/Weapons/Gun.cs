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
    public GameObject rocketPrefab;
    public float cooldown;
    public float ammo;
    public float maxAmmo;
    public int damage;

    // Local Variables
    Transform target;
    float currentCooldown;

    #region Setup
    private void Awake()
    {
        foreach(ParticleSystem n in GetComponentsInChildren<ParticleSystem>())
        {
            particles.Add(n);
        }
    }
    #endregion Setup

    private void Start()
    {
        if (type == GunType.RocketLauncher)
            CallbackHandler.instance.setRocketTarget += SetRocketTarget;

        ammo = maxAmmo;
    }
    private void OnDestroy()
    {
        if (type == GunType.RocketLauncher)
            CallbackHandler.instance.setRocketTarget -= SetRocketTarget;
    }

    public void SetRocketTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;

        CallbackHandler.instance.UpdateAmmo(ammo, maxAmmo);

        if (ammo <= 0.0f)
        {
            AudioHandler.instance.ToggleLoopingSound(type.ToString(), false);
            AudioHandler.instance.PlayAudio("OutOfAmmo");
            this.gameObject.SetActive(false);
            ToggleParticles(false);
            return;
        }

        switch (type)
        {
            case GunType.RocketLauncher:
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentCooldown <= 0.0f && ammo > 0)
                    {
                        AudioHandler.instance.PlayAudio("RocketLauncherShoot");
                        ToggleParticles(true);                   
                        Instantiate(rocketPrefab, transform.position, transform.rotation, null).GetComponent<RocketProjectile>().SetTarget(target, damage);

                        currentCooldown = cooldown;
                        ammo--;

                        if (ammo <= 0)
                            CallbackHandler.instance.ToggleRocket(false);
                    }
                }
                ToggleParticles(false);
                break;
            }
            default:
            {
                if (Input.GetKey(KeyCode.E) && ammo > 0.0f)
                {
                    AudioHandler.instance.ToggleLoopingSound(type.ToString(), true);
                    ToggleParticles(true);
                    ammo -= Time.deltaTime;
                    break;
                }
                ToggleParticles(false);
                AudioHandler.instance.ToggleLoopingSound(type.ToString(), false);
            }
            return;
        }
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
        if (_type == type)
            ammo = maxAmmo;

        CallbackHandler.instance.ToggleRocket(false);
        if (_type == type && type == GunType.RocketLauncher)
        {
            CallbackHandler.instance.ToggleRocket(_type == type);
        }
    }

    public void UpgradeGun()
    {
        switch (type)
        {
            case GunType.MachineGun:
            {
                damage += Mathf.RoundToInt(damage / 5.0f);
                maxAmmo += Mathf.RoundToInt(maxAmmo / 5.0f);
                ammo = maxAmmo;
                break;
            }
            case GunType.FlameThrower:
            {
                maxAmmo += Mathf.RoundToInt(maxAmmo / 5.0f);
                ammo = maxAmmo;
                break;
            }
            case GunType.RocketLauncher:
            {
                cooldown -= 0.2f;
                maxAmmo += 2;
                ammo = maxAmmo;
                break;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioHandler exists!");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    [Header("Powerups & Upgrades")]
    public AudioSource chassisUpgrade;
    public AudioSource weaponsUpgrade;
    public AudioSource engineUpgrade;
    [Header("Weapons")]
    public AudioSource machineGun;
    public AudioSource flameThrower;
    public AudioSource rocketLauncher;
    public AudioSource machineGunShoot;
    public AudioSource flameThrowerShoot;
    public AudioSource rocketLauncherShoot;
    public AudioSource outOfAmmo;
    [Header("Countdown")]
    public AudioSource three;
    public AudioSource two;
    public AudioSource one;
    public AudioSource go;

    public void PlayAudio(string _command)
    {
        switch (_command)
        {
            case "MachineGun Online":
            {
                machineGun.PlayOneShot(machineGun.clip);
                break;
            }
            case "FlameThrower Online":
            {
                flameThrower.PlayOneShot(flameThrower.clip);
                break;
            }
            case "RocketLauncher Online":
            {
                rocketLauncher.PlayOneShot(rocketLauncher.clip);
                break;
            }
            case "Chassis Upgrade":
            {
                chassisUpgrade.PlayOneShot(chassisUpgrade.clip);
                break;
            }
            case "Weapons Upgrade":
            {
                weaponsUpgrade.PlayOneShot(weaponsUpgrade.clip);
                break;
            }
            case "Engine Upgrade":
            {
                engineUpgrade.PlayOneShot(engineUpgrade.clip);
                break;
            }
            case "Three":
            {
                three.PlayOneShot(three.clip);
                break;
            }
            case "Two":
            {
                two.PlayOneShot(two.clip);
                break;
            }
            case "One":
            {
                one.PlayOneShot(one.clip);
                break;
            }
            case "Go":
            {
                go.PlayOneShot(go.clip);
                break;
            }
            case "RocketLauncherShoot":
            {
                rocketLauncherShoot.PlayOneShot(rocketLauncherShoot.clip);
                break;
            }
            case "OutOfAmmo":
            {
                outOfAmmo.PlayOneShot(outOfAmmo.clip);
                break;
            }

            default:
                break;
        }
    }

    public void ToggleLoopingSound(string _string, bool _toggle)
    {
        switch (_string)
        {
            case "MachineGun":
            {
                machineGunShoot.volume = _toggle ? 1.0f : 0.0f;
                break;
            }
            case "FlameThrower":
            {
                flameThrowerShoot.volume = _toggle ? 1.0f : 0.0f;
                break;
            }
        }
    }
}

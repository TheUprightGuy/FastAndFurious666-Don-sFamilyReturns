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
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Chassis,
    Engine,
    Weapons
}

public class Upgrade : MonoBehaviour
{
    public UpgradeType type;

    private void OnTriggerEnter(Collider other)
    {
        Movement temp = other.GetComponent<Movement>();
        // Temp to check if player
        if (temp)
        {
            AudioHandler.instance.PlayAudio(type.ToString() + " Upgrade");

            switch (type)
            {
                case UpgradeType.Chassis:
                {
                    HealthAttribute health = other.GetComponent<HealthAttribute>();
                    health.maxHealth += 50;
                    health.health = health.maxHealth;
                        health.TakeDamage(0, false);

                    break;
                }
                // Upgrade Max Speed & Acceleration
                case UpgradeType.Engine:
                {
                    temp.maxSpeed += 10;
                    temp.speedForce *= 1.1f;

                    break;
                }
                case UpgradeType.Weapons:
                {
                    foreach (Gun n in temp.GetComponentsInChildren<Gun>())
                    {
                        n.UpgradeGun();
                    }
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

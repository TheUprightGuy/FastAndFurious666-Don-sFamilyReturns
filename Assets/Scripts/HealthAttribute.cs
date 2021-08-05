using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAttribute : MonoBehaviour
{
    [Header("Setup Variables")]
    public GameObject explosionPrefab;
    public int maxHealth;
    //[HideInInspector] 
    [Header("Debug Values")]
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(other.GetComponentInParent<Gun>().damage);
        Movement player = other.GetComponentInParent<Movement>();
        if (player)
        {
            CarlAI temp = GetComponent<CarlAI>();
            if (temp)
            {
                temp.ToggleHostile(true, player.transform);
                Debug.Log(transform.gameObject.name + " Aggros on player");
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        //Debug.Log("Took " + _damage + " damage.");

        health -= _damage;
        if (health <= 0)
        {
            GameObject pfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
            Destroy(pfx, 4.0f);

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<HealthAttribute>())
            return;

        Movement player = collision.gameObject.GetComponent<Movement>();
        if (player)
        {
            CarlAI temp = GetComponent<CarlAI>();
            if (temp)
            {
                temp.ToggleHostile(true, player.transform);
                Debug.Log(transform.gameObject.name + " Aggros on player");
            }
        }

        TakeDamage(1);
    }
}

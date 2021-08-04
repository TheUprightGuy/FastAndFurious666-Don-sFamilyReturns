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
    }

    public void TakeDamage(int _damage)
    {
        Debug.Log("Took " + _damage + " damage.");

        health -= _damage;
        if (health <= 0)
        {
            GameObject pfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
            Destroy(pfx, 4.0f);

            Destroy(this.gameObject);
        }
    }
}

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
        Movement player = other.GetComponentInParent<Movement>();
        TakeDamage(other.GetComponentInParent<Gun>().damage, true);
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

    public void TakeDamage(int _damage, bool _player)
    {
        //Debug.Log("Took " + _damage + " damage.");

        health -= _damage;

        if (GetComponent<Movement>())
        {
            CallbackHandler.instance.UpdateHealth((float)health / (float)maxHealth);
        }


        if (health <= 0)
        {
            GameObject pfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
            Destroy(pfx, 4.0f);

            // Check to see if Player killed an AI
            if (_player)
                CallbackHandler.instance.HasKilled();

            // Check if player was Killed
            Movement player = GetComponent<Movement>();
            if (player)
                CallbackHandler.instance.ShowEndScreen(EndState.Lose);

            // Check to see if any AI are alive
            if (!player && !CallbackHandler.instance.CheckSurvivors())
                CallbackHandler.instance.ShowEndScreen(EndState.Win);

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

        TakeDamage(1, false);
    }
}

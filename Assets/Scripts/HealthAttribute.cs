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
            {
                Invoke("ShowDeath", 2.0f);
                CallbackHandler.instance.ToggleFreeze(true);            
            }
            else
            {
                CallbackHandler.instance.DoubleCheckSurvivors();
                Destroy(this.gameObject);
            }  
        }
    }   

    void ShowDeath()
    {
        CallbackHandler.instance.ShowEndScreen(EndState.Lose);// Delay - Go to next screen, show ty message
        CallbackHandler.instance.DisplayThankYou();
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

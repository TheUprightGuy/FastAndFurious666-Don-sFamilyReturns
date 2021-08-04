using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GunType type;

    private void Start()
    {
        // Probably needs to be seeded correctly
        int rand = Random.Range(0, 3);
        type = (GunType)rand;
    }

    private void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * 40.0f + transform.right * Time.deltaTime * 40.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapons temp = other.GetComponent<Weapons>();

        // Temp to check if player
        if (temp)
        {
            temp.EnableWeapon(type);
            AudioHandler.instance.PlayAudio(type.ToString() + " Online");
            Destroy(this.gameObject);
        }
    }
}

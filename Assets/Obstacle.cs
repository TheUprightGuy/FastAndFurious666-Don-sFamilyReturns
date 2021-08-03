using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnParticleCollision(GameObject other)
    {
        GameObject pfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
        Destroy(pfx, 4.0f);

        Destroy(this.gameObject);
    }
}

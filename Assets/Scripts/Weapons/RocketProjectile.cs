using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    [Header("Setup Fields")]
    public float baseSpeed = 15.0f;
    public GameObject explosionPrefab;

    // Local Variables
    Rigidbody rb;
    float speed;
    Transform target;
    int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    public void SetTarget(Transform _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;

        Vector3 dir = Vector3.Normalize(new Vector3(target.position.x, 0, target.position.z) - new Vector3(this.transform.position.x, 0, this.transform.position.z));
        rb.MoveRotation(Quaternion.LookRotation(dir));

        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.name);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
        Destroy(explosion, 4.0f);
        other.GetComponent<HealthAttribute>().TakeDamage(damage, true);

        Destroy(this.gameObject);
    }
}

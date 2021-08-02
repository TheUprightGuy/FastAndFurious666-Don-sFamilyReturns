using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speedForce;
    public float maxSpeed;

    public float velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.W))
        {
            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(transform.forward * speedForce);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.magnitude > 0)
                rb.velocity *= (1 - Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * 100.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 100.0f);
        }
    }
}

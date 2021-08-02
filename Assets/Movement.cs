using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    float angle;

    public float speedForce;
    public float maxSpeed;

    public Transform car;
    public float carRotation = 0.0f;

    public Transform targetPos;
    float maxDistance;

    List<TrailRenderer> skidMarks = new List<TrailRenderer>();
    List<ParticleSystem> skidClouds = new List<ParticleSystem>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        foreach(TrailRenderer n in GetComponentsInChildren<TrailRenderer>())
        {
            skidMarks.Add(n);
        }
        foreach (ParticleSystem n in GetComponentsInChildren<ParticleSystem>())
        {
            skidClouds.Add(n);
        }
    }

    private void Start()
    {
        maxDistance = Vector3.Distance(transform.position, targetPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        angle = Vector3.Angle(transform.forward, rb.velocity.normalized);


        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.magnitude > 0)
                rb.velocity *= (1 - Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) )
        {
            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(transform.forward * speedForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(-transform.forward * speedForce * Time.deltaTime * 0.75f);
        }


        carRotation = Mathf.Clamp(carRotation, -20.0f, 20.0f);
        car.localRotation = Quaternion.Euler(0, carRotation, 0);
        ToggleSkids(angle > 40.0f);


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * 100.0f * Mathf.Clamp01(rb.velocity.magnitude / 10));

            carRotation -= Mathf.Clamp01(rb.velocity.magnitude / 10.0f);
            car.localRotation = Quaternion.Euler(0, carRotation, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 100.0f * Mathf.Clamp01(rb.velocity.magnitude / 10));

            carRotation += Mathf.Clamp01(rb.velocity.magnitude / 10.0f);
            car.localRotation = Quaternion.Euler(0, carRotation, 0);
        }
        else
        {
            carRotation = Mathf.Lerp(carRotation, 0, Time.deltaTime * 3.0f);
        }

        CallbackHandler.instance.UpdateSpeedometer(rb.velocity.magnitude, maxSpeed);
        CallbackHandler.instance.UpdateProgress(Vector3.Distance(transform.position, targetPos.position), maxDistance);
    }

    void ToggleSkids(bool _toggle)
    {
        foreach(TrailRenderer n in skidMarks)
        {
            n.emitting = _toggle;
        }

        foreach (ParticleSystem n in skidClouds)
        {
            n.enableEmission = _toggle;
            n.transform.localRotation = Quaternion.Euler(new Vector3(270 - car.localRotation.eulerAngles.y * 3.0f, -90.0f, 90.0f));
        }
    }
}

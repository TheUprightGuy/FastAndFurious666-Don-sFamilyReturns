using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{

    [Header("Setup Variables")]
    public Transform car;
    [Header("Speed Variables")]
    public float speedForce;
    public float maxSpeed;
    [Header("Objective Tracking - TEMP")]
    public Transform targetPos;
    float maxDistance;

    // Local Variables
    Rigidbody rb;
    public float angle;
    float carRotation = 0.0f;
    bool reversing;
    // Skid Marks + Dust PFX
    List<TrailRenderer> skidMarks = new List<TrailRenderer>();
    public List<ParticleSystem> skidClouds;

    #region Setup
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        foreach(TrailRenderer n in GetComponentsInChildren<TrailRenderer>())
        {
            skidMarks.Add(n);
        }
    }
    #endregion Setup

    private void Start()
    {
        // Get distance from objective - temp
        maxDistance = Vector3.Distance(transform.position, targetPos.position);
    }

    void Update()
    {
        // Get Angle Between Forward + Current Velocity
        angle = Vector3.Angle(transform.forward, rb.velocity.normalized);
        //reversing = angle > 115.0f;
        reversing = false;

        // TEMP INPUT
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
            reversing = true;

            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(-transform.forward * speedForce * Time.deltaTime * 0.75f);
        }

        // Rotation & Skids
        carRotation = Mathf.Clamp(carRotation, -20.0f, 20.0f);
        car.localRotation = Quaternion.Euler(0, carRotation, 0);
        ToggleSkids(angle > 40.0f);


        // TEMP INPUT
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate((reversing ? -1 : 1) * -Vector3.up * Time.deltaTime * 100.0f * Mathf.Clamp01(rb.velocity.magnitude / 10));

            carRotation -= Mathf.Clamp01(rb.velocity.magnitude / 10.0f);
            car.localRotation = Quaternion.Euler(0, carRotation, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate((reversing ? -1 : 1) * Vector3.up * Time.deltaTime * 100.0f * Mathf.Clamp01(rb.velocity.magnitude / 10));

            carRotation += Mathf.Clamp01(rb.velocity.magnitude / 10.0f);
            car.localRotation = Quaternion.Euler(0, carRotation, 0);
        }
        else
        {
            carRotation = Mathf.Lerp(carRotation, 0, Time.deltaTime * 3.0f);
        }

        // UPDATE UI ELEMENTS
        CallbackHandler.instance.UpdateSpeedometer(rb.velocity.magnitude, maxSpeed);
        CallbackHandler.instance.UpdateProgress(Vector3.Distance(transform.position, targetPos.position), maxDistance);
    }

    // Toggle on Skids & PFX
    void ToggleSkids(bool _toggle)
    {
        foreach(TrailRenderer n in skidMarks)
        {
            n.emitting = _toggle;
        }

        foreach (ParticleSystem n in skidClouds)
        {
            if (_toggle)
            {
                n.Play();
                n.transform.localRotation = Quaternion.Euler(new Vector3(270 - car.localRotation.eulerAngles.y * 3.0f, -90.0f, 90.0f));
                return;
            }
            n.Stop();
        }
    }
}

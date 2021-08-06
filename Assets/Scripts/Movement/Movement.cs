using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    [Header("Setup Variables")]
    public Transform car;
    public Material brakingMat;
    [Header("Speed Variables")]
    public float speedForce;
    public float maxSpeed;

    // Local Variables
    Rigidbody rb;
    float angle;
    float carRotation = 0.0f;
    bool reversing;
    CarAudio audio;
    bool freeze = true;
    // Skid Marks + Dust PFX
    List<TrailRenderer> skidMarks = new List<TrailRenderer>();
    public List<ParticleSystem> skidClouds;

    #region Setup
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponentInChildren<CarAudio>();
        foreach(TrailRenderer n in GetComponentsInChildren<TrailRenderer>())
        {
            skidMarks.Add(n);
        }
    }
    #endregion Setup

    private void Start()
    {
        CallbackHandler.instance.toggleFreeze += ToggleFreeze;
        audio.SetSkidding(false);
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.toggleFreeze -= ToggleFreeze;
    }

    public void ToggleFreeze(bool _toggle)
    {
        freeze = _toggle;
    }


    void Update()
    {
        if (freeze)
            return;

        // Get Angle Between Forward + Current Velocity
        angle = Vector3.Angle(transform.forward, rb.velocity.normalized);
        reversing = false;
        brakingMat.color = Color.white;


        // TEMP INPUT
        if (Input.GetKey(KeyCode.Space))
        {
            brakingMat.color = Color.red;

            if (rb.velocity.magnitude > 0)
            {
                rb.velocity *= (1 - Time.deltaTime);
                audio.SetSkidding(true);
            }
        }
        else if (Input.GetKey(KeyCode.W) )
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                float perc = rb.velocity.magnitude / maxSpeed;
                rb.AddForce(transform.forward * speedForce * Time.deltaTime);
                rb.AddForce(transform.forward * speedForce * Time.deltaTime * (1 - perc));
            }
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
        ToggleSkids(angle > 40.0f && rb.velocity.magnitude > 0.3f);


        // TEMP INPUT
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate((reversing ? -1 : 1) * -Vector3.up * Time.deltaTime * 100.0f * Mathf.Clamp01(rb.velocity.magnitude / 10));

            carRotation -= Mathf.Clamp01(rb.velocity.magnitude / 10.0f);
            car.localRotation = Quaternion.Euler(0, carRotation, 0);
        }
        else if (Input.GetKey(KeyCode.D))
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
        audio.UpdateEngine(rb.velocity.magnitude / maxSpeed);
    }

    private void FixedUpdate()
    {
        Quaternion rot = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.rotation = rot;
    }

    // Toggle on Skids & PFX
    void ToggleSkids(bool _toggle)
    {
        audio.SetSkidding(_toggle);

        foreach (TrailRenderer n in skidMarks)
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

    private void OnCollisionEnter(Collision collision)
    {
        // Check if hit something destructible
        HealthAttribute temp = collision.gameObject.GetComponent<HealthAttribute>();

        if (temp)
        {
            audio.PlayCrashAudio(rb.velocity.magnitude / maxSpeed);
        }
    }
}

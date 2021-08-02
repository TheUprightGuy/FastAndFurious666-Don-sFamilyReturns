using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speedForce;
    public float maxSpeed;

    public Transform car;
    public float carRotation = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}

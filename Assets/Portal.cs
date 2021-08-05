using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
            Debug.Log("Went through portal");
    }
}

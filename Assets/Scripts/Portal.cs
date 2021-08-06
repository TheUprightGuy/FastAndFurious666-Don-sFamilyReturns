using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 destination;

    private void OnTriggerEnter(Collider other)
    {
        // TEMP
        if (other.GetComponent<Movement>())
            CallbackHandler.instance.ShowEndScreen(EndState.Killed);
    }
}

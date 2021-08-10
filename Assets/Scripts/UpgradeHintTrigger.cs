using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHintTrigger : MonoBehaviour
{
    void CancelHint()
    {
        CallbackHandler.instance.ToggleHint(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            CallbackHandler.instance.ToggleHint(true);
            Invoke("CancelHint", 5.0f);
        }
    }
}

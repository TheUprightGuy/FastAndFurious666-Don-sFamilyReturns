using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLevelSwitcher : MonoBehaviour
{
    public Portal FirstPortal;
    public EndPortal LastPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadMultiply))
        {
            First();
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            Last();
        }
    }
    void First()
    {
        FirstPortal.Placings.Add(FirstPortal.player);
        FirstPortal.TriggerPortal();
    }

    void Last()
    {
        FirstPortal.Placings.Add(FirstPortal.player);
        FirstPortal.TriggerPortal();
        LastPortal.TriggerPortal();
        LastPortal.StartArena();
    }
}

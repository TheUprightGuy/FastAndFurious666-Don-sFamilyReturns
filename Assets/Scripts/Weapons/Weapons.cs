using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    List<Gun> guns = new List<Gun>();

    private void Awake()
    {
        foreach(Gun n in GetComponentsInChildren<Gun>())
        {
            guns.Add(n);
        }
    }

    private void Start()
    {
        EnableWeapon(GunType.None);
    }

    // Callback would probably be better here
    public void EnableWeapon(GunType _type)
    {
        foreach(Gun n in guns)
        {
            n.ToggleWeapon(_type);
        }
    }
}

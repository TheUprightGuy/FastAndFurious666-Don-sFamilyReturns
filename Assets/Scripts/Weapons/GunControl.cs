using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}

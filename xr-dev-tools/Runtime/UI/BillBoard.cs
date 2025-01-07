using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BillBoard : MonoBehaviour
{
    void Update()
    {
        Vector3 lookPosition = Camera.main.transform.position;
        transform.LookAt(new Vector3(lookPosition.x, transform.position.y, lookPosition.z));

        // rotate 180 degrees so front faces camera
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
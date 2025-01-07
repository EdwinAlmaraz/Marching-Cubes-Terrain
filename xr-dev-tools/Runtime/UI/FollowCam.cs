using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float Height = 0;
    public Transform cam;
    public float cameraDistance = .04f;

    // Start is called before the first frame update
    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            transform.position = cam.position + cam.forward * cameraDistance;
            transform.LookAt(cam);            
        }
    }
}

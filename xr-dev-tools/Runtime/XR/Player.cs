using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public XROrigin XROrigin { get; private set; } = null;
    public Camera PlayerCamera { get; private set; } = null;
    public XRDirectInteractor LeftHandGrabber;
    public XRDirectInteractor RightHandGrabber;
    public XRRayInteractor LeftRay;
    public XRRayInteractor RightRay;
    public Hand LeftHand;
    public Hand RightHand;

    private void Awake()
    {
        instance = this;

        XROrigin xrorigin = GetComponent<XROrigin>();
        if (xrorigin != null)
        {
            XROrigin = xrorigin;
            PlayerCamera = xrorigin.Camera;
        }        
        else
        {
            PlayerCamera = Camera.main;
        }
    }
}

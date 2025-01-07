using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TeleportLaser : MonoBehaviour
{
    [SerializeField] private GameObject OtherTeleportRay;
    [SerializeField] private GameObject RegularLaserPointer;
    [SerializeField] private InputActionReference ActivateTeleportLine;
    private XRInteractorLineVisual lineVisual;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        ActivateTeleportLine.action.started += Action_started;
        ActivateTeleportLine.action.canceled += Action_canceled;
        
    }

    private void Start()
    {
        lineVisual = GetComponent<XRInteractorLineVisual>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        if (lineVisual != null) 
            lineVisual.enabled = true;
        if (lineRenderer != null)
            lineRenderer.enabled = true;
        if (OtherTeleportRay != null)
            OtherTeleportRay.SetActive(false);
        if (RegularLaserPointer != null)
            RegularLaserPointer.SetActive(false);


    }
    private void Action_canceled(InputAction.CallbackContext obj)
    {
        if (lineVisual.reticle != null)
        {
            lineVisual.reticle.SetActive(false);
        }

        if (lineVisual != null)
            lineVisual.enabled = false;

        if (lineRenderer != null)
            lineRenderer.enabled = false;
        
        if (RegularLaserPointer != null)
            RegularLaserPointer.SetActive(true);

        if (OtherTeleportRay != null)        {
            OtherTeleportRay.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class InGameMenu_Control : MonoBehaviour
{
    [System.Serializable]
    public struct MenuPositioningInfo
    {
        public Transform cameraTransform;
        public Vector2 cameraOffset;
    }

    public MenuPositioningInfo positioningInfo;
    public InputActionReference menuButtonReference = null;
    public GameObject InGameMenu;
    
    private void Awake()
    {
        menuButtonReference.action.started += Toggle;
    }

    private void Start()
    {
        if (positioningInfo.cameraTransform == null)
        {
            positioningInfo.cameraTransform = Camera.main.transform;
        }
    }

    private void OnDestroy()
    {
        menuButtonReference.action.started -= Toggle;
    }
    
    private void Toggle(InputAction.CallbackContext context)
    {
        transform.SetWorldPose(GetLockedPose(positioningInfo));
        bool isActive = !InGameMenu.activeSelf;
        InGameMenu.SetActive(isActive);
    }

    public void ChangeScene(int sceneIndex)
    {
        Fader.instance.dimmerObj.SetActive(true);
        Fader.instance.dimmerObj.GetComponent<Dimmer>().Fade(true, 1f);
        StartCoroutine(DelayedLoadScene(sceneIndex, 2f));
    }

    IEnumerator DelayedLoadScene(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void ToggleCCButton()
    { 
        LanguageManager.instance.showClosedCaptioning = !LanguageManager.instance.showClosedCaptioning;
        ClosedCaptioning.canvas.enabled =LanguageManager.instance.showClosedCaptioning;
    }

    public static Pose GetLockedPose(MenuPositioningInfo positioningInfo)
    {
        Transform cameraTransform = positioningInfo.cameraTransform;
        Vector2 cameraOffset = positioningInfo.cameraOffset;

        Vector3 newPosition = cameraTransform.position + cameraTransform.forward * cameraOffset.x;
        newPosition.y = cameraTransform.position.y + cameraOffset.y;

        float yRotation = cameraTransform.eulerAngles.y;
        Quaternion newRotation = Quaternion.Euler(0, yRotation, 0);

        Pose result = new Pose(newPosition, newRotation);

        return result;
    }
}

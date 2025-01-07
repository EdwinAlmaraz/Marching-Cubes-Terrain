using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Use this class directly on the TextMeshProUGUI on the Main Menu canvas to automatically update the version number to match what is set in the project settings.
public class AutoUpdateVersion : MonoBehaviour
{
    private TextMeshProUGUI VersionText;
    // Start is called before the first frame update
    void Start()
    {
        VersionText = GetComponent<TextMeshProUGUI>();
        VersionText.text = "Version: "+ Application.version;
    }

}

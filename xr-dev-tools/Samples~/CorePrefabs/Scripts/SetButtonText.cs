using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetButtonText : MonoBehaviour
{
    public TextMeshProUGUI Text;


    // Start is called before the first frame update
    public enum TextOptions
    {
        Tutorial,
        OrderFilling,
        Trip1,
        Trip2,
        StartTrip,
        TruckLoading,
        Continue,
        Exit,
        ExitCourse,
        ClosedCaptions,
        SkipVoice,
        SkipLocation,
        ResetCase,
        Return,
        No,
        Yes
    }
    public TextOptions myText;

    // Update is called once per frame
    void Update()
    {
        switch (myText)
        {
            case TextOptions.Tutorial:
                Text.text = "Tutorial";
                break;
            case TextOptions.Trip1:
                Text.text = "Trip 1";
                break;
            case TextOptions.Trip2:
                Text.text = "Trip 2";
                break;
            case TextOptions.StartTrip:
                Text.text = "Starting Trip";
                break;
            case TextOptions.OrderFilling:
                Text.text = "Order Filling";
                break;
            case TextOptions.TruckLoading:
                Text.text = "Truck Loading";
                break;
            case TextOptions.Continue:
                Text.text = "Continue";
                break;
            case TextOptions.Exit:
                Text.text = "Exit";
                break;
            case TextOptions.ExitCourse:
                Text.text = "Exit Course";
                break;
            case TextOptions.ResetCase:
                Text.text = "Reset Case";
                break;
            case TextOptions.SkipLocation:
                Text.text = "Skip Location";
                break;
            case TextOptions.SkipVoice:
                Text.text = "Skip Voice System";
                break;
            case TextOptions.ClosedCaptions:
                Text.text = "Closed Captions";
                break;
            case TextOptions.Return:
                Text.text = "Return";
                break;
            case TextOptions.No:
                Text.text = "No";
                break;
            case TextOptions.Yes:
                Text.text = "Yes";
                break;
            default:
                break;
        }
    }
}

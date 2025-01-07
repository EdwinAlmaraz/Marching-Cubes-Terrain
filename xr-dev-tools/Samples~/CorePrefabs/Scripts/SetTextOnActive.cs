using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetTextOnActive : MonoBehaviour
{
    public enum MenuItemText
    {
        PressXButton,
        PressThumbstick,
        PressGripTrigger,
        BrickLayerNotice,
        ColumnStackNotice,
        TooHighScreen,
        TooHeavyScreen,
        MovePowerEquipment,
        BrokenCase,
        CourseComplete,
        ExitConfirmation,
        RecenterScreenIntroSlide,
        RecenterInstructions,
        RecenterConfirmation,
        ExitTripConfirmation,
        FifteenMinuteMark
    }

    public MenuItemText ItemText;

    public TextMeshProUGUI Text;
    private void OnEnable()
    {
        switch (ItemText)
        {
            case MenuItemText.PressXButton:

                Text.text = "Press the X button on the left controller to continue.";


                break;
            case MenuItemText.PressThumbstick:

                Text.text = "Press the Menu button on either controller to continue.";

                break;
            case MenuItemText.PressGripTrigger:

                Text.text = "Place both controllers on the box, then squeeze and hold both grip triggers.";

                break;
            case MenuItemText.BrickLayerNotice:

                Text.text = "These boxes must be brick layered when stacked.";

                break;
            case MenuItemText.ColumnStackNotice:

                Text.text = "RPCs must be column stacked.";

                break;
            case MenuItemText.TooHighScreen:

                Text.text = "You are stacking too high! Try to fill up each row before moving on to the next. Move this case to a different location on the pallet.";

                break;
            case MenuItemText.TooHeavyScreen:

                Text.text = "This box is too heavy to be placed here. Move the box to a better location on the pallet.";

                break;
            case MenuItemText.MovePowerEquipment:

                Text.text = "Kindly move your power equipment to the side to let others pass.";


                break;
            case MenuItemText.BrokenCase:

                Text.text = "A box seems to have fallen off of the conveyor belt. We will have to clean this up before continuing.";

                break;
            case MenuItemText.CourseComplete:

                Text.text = "Great job, you have finished this trip. Press continue to be taken back to the main menu.";

                break;
            case MenuItemText.ExitConfirmation:

                Text.text = "Are you sure you want to exit the course?";

                break;
            case MenuItemText.RecenterScreenIntroSlide:
                Text.text = "Look at the floor and locate the circle shown below. Stand in the center of the circle and follow the instructions that appear.";


                break;
            case MenuItemText.RecenterInstructions:

                Text.text = "Turn your body and head until the yellow bar lines up with the bottom edge of the grid. \r\n Then, choose continue.";

                break;
            case MenuItemText.RecenterConfirmation:

                Text.text = "Are you happy with the current orientation?";

                break;
            case MenuItemText.ExitTripConfirmation:

                Text.text = "Are you sure you want to exit this scenario?";

                break;
            case MenuItemText.FifteenMinuteMark:

                Text.text = "You have been in the experience for 15 minutes.  You should take a short break.  Don't forget to charge your headset if you need to.";

                break;

            default:
                break;



        }
    }
}

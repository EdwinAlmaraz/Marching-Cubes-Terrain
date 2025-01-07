using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTL.Atlas
{
    public class FadeInSequence : AbstractFadeSequence
    {
        // Fade To Transparent
        public FadeInSequence(Dimmer fadeScreen, float fadeTime, bool blockUntilFinished = true) : base(fadeScreen, fadeTime, blockUntilFinished) { }
        public FadeInSequence(float fadeTime, bool blockUntilFinished = true) : base(fadeTime, blockUntilFinished) { }
        protected override void AddStates()
        {
            if (fadeScreen == null)
            {
                fadeScreen = Fader.instance.dimmerScreen.gameObject.GetComponent<Dimmer>();
            }

            AddState(new FadeState(false, fadeTime, fadeScreen));

            if (blockUntilFinished)
                AddState(new DelayState(fadeTime));

            AddState(new SetActiveState(fadeScreen, false));

            AddState(new EndState());
            FinishStateMachine();
        }
    }
}
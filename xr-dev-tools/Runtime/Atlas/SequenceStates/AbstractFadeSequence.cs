using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTL.Atlas
{
    public abstract class AbstractFadeSequence : SequenceState
    {
        protected Dimmer fadeScreen;
        protected float fadeTime;
        protected bool blockUntilFinished;

        protected AbstractFadeSequence(Dimmer fadeScreen, float fadeTime, bool blockUntilFinished = true) : base()
        {
            this.fadeScreen = fadeScreen;
            this.fadeTime = fadeTime;
            this.blockUntilFinished = blockUntilFinished;

            AddStates();
        }

        protected AbstractFadeSequence(float fadeTime, bool blockUntilFinished = true) : base()
        {
            this.fadeScreen = null;
            this.fadeTime = fadeTime;
            this.blockUntilFinished = blockUntilFinished;

            AddStates();
        }

        protected abstract void AddStates();
    }
}
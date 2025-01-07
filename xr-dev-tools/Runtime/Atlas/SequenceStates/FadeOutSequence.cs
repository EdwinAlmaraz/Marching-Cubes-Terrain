namespace RTL.Atlas
{
    public class FadeOutSequence : AbstractFadeSequence
    {
        // Fade To Black
        public FadeOutSequence(Dimmer fadeScreen, float fadeTime, bool blockUntilFinished = true) : base(fadeScreen, fadeTime, blockUntilFinished) { }
        public FadeOutSequence(float fadeTime, bool blockUntilFinished = true) : base(fadeTime, blockUntilFinished) { }
        protected override void AddStates()
        {
            if (fadeScreen == null)
            {
                fadeScreen = Fader.instance.dimmerScreen.gameObject.GetComponent<Dimmer>();
            }

            AddState(new SetActiveState(fadeScreen));
            AddState(new FadeState(true, fadeTime, fadeScreen));

            if (blockUntilFinished)
                AddState(new DelayState(fadeTime));

            AddState(new EndState());
            FinishStateMachine();
        }
    }
}
namespace RTL.Atlas
{
    public class FadeState : State
    {
        private bool _fadeTo;
        private float _fadeTime;
        private Dimmer _dimmer;

        public FadeState(bool fadeDark, float fadeTime, Dimmer dimmer)
        {
            _fadeTo = fadeDark;
            _fadeTime = fadeTime;
            _dimmer = dimmer;
        }

        public override void Init()
        {
            _dimmer.Fade(_fadeTo, _fadeTime);
            SelectNextState();
        }

        public override void Update(float timer)
        {

        }
    }
}
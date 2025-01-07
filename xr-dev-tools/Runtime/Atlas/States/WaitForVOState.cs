namespace RTL.Atlas
{
    public class WaitForVOState : WaitForAudioState
    {
        public WaitForVOState() : base(Narrator.instance.audioSource) { }
    }
}
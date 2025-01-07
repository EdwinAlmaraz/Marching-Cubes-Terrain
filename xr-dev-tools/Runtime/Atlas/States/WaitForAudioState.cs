using UnityEngine;

namespace RTL.Atlas
{
    public class WaitForAudioState : State
    {
        protected AudioSource audioSource;

        public WaitForAudioState(AudioSource audioSource = null)
        {
            if (audioSource == null) 
            {
                this.audioSource = Narrator.instance.audioSource;
            }
            else
            {
                this.audioSource = audioSource;
            }
        }

        public override void Init() { }

        public override void Update(float timer)
        {
            if (!audioSource.isPlaying)
                SelectNextState();
        }
    }
}
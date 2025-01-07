using UnityEngine;

namespace RTL.Atlas
{
    public class PlayAudioState : State
    {
        private AudioSource audioSource;
        private AudioClip clip;
        private bool blockUntilFinished;

        public PlayAudioState(AudioSource audioSource, AudioClip clip, bool blockUntilFinished = false)
        {
            if (audioSource == null)
                Debug.LogError("Invalid Audio Source");
            if (clip == null)
                Debug.LogError("Invalid Audio Clip");

            this.audioSource = audioSource;
            this.clip = clip;
            this.blockUntilFinished = blockUntilFinished;
        }

        public override void Init()
        {
            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();

            if (audioSource != null && clip != null)
                audioSource.PlayOneShot(clip);

            if (!blockUntilFinished)
                SelectNextState();
        }

        public override void Update(float timer)
        {
            if (blockUntilFinished)
                if (!audioSource.isPlaying)
                    SelectNextState();
        }
    }
}
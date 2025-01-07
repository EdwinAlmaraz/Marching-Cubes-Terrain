// Author(s):       Edwin Almaraz, Max Calhoun
// Updated:         2/3/2023
// Description:     SequenceState used in scene sequence controller to play audioclip with given key from language manager.  It also sets the current closed captioning text.
//                  waitForAudio = true, waits for audiosource to stop playing audio before proceeding to the next state.

using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RTL.Atlas
{
    public class PlayVOSequence : SequenceState
    {
        TextMeshProUGUI uiText;
        string message;

        AudioSource audioSource;
        AudioClip audioClip;

        bool waitForAudio;

        public PlayVOSequence(string key, bool waitForAudio = false) : base()
        {
            Dictionary<string, VOLine> voiceOverLines = LanguageManager.instance.getVOLine(key);
            if (voiceOverLines == null)
            {
                Debug.LogError("Invalid Key: " + key);
            }
            else
            {
                message = voiceOverLines[LanguageManager.instance.currentLanguage].text;
                audioClip = voiceOverLines[LanguageManager.instance.currentLanguage].audioClip;
            }

            audioSource = Narrator.instance.audioSource;
            uiText = ClosedCaptioning.text;
            this.waitForAudio = waitForAudio;

            AddStates();
        }

        protected void AddStates()
        {
            // TODO: should this always set the CC Text even if showClosedCaptioning is false?  Reasoning: when learner wants to turn on CC because they missed the VO...
            IF(() => LanguageManager.instance.showClosedCaptioning);
            {
                AddState(new SetTextState(uiText, message));
            }
            ENDIF();

            AddState(new PlayAudioState(audioSource, audioClip, waitForAudio));

            AddState(new EndState());
            FinishStateMachine();
        }
    }
}
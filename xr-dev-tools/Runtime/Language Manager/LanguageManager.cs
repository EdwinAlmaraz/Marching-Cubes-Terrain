using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLine
{
    public AudioClip audioClip { get; private set; }
    public string text { get; private set; }

    public VOLine(AudioClip audioClip, string text)
    {
        this.audioClip = audioClip;
        this.text = text;
    }
}

public class LanguageManager
{
    private static LanguageManager instance_ = null;
    public static LanguageManager instance
    {
        get
        {
            if (instance_ == null)
                instance_ = new LanguageManager();

            return instance_;
        }
    }

    public event System.Action<bool> toggleClosedCaptioning;

    private bool _showClosedCaptioning = true;
    public bool showClosedCaptioning 
    { 
        get { return _showClosedCaptioning; }
        set
		{
            _showClosedCaptioning = value;
            toggleClosedCaptioning?.Invoke(_showClosedCaptioning);
		}
    }

    public List<string> supportedLanguages { get; private set; }
    public string currentLanguage { get; set; }

    // Language Specific Data
    private Dictionary<string, Dictionary<string, VOLine>> voiceOverLines = new Dictionary<string, Dictionary<string, VOLine>>();
    //private Dictionary<string, Dictionary<string, string>> uiText = new Dictionary<string, Dictionary<string, string>>();

    private LanguageManager()
    { 
        // Load VOInfo.JSON
        TextAsset textAsset = Resources.Load<TextAsset>("VOInfo");
        if (textAsset == null)
            Debug.LogError("Unable to Load VOInfo.JSON");

        // Load JSON data from VOInfo.JSON
        VOFileLocations VOInfo = JsonUtility.FromJson<VOFileLocations>(textAsset.text);
        if (VOInfo == null)
            Debug.LogError("Unable to Load JSON data from VOInfo.JSON");

        // load Supported Languages
        if (VOInfo.supportedLanguages == null)
        {
            Debug.LogError("Unable to find any supported languages in VOInfo.JSON");
        }
        else
        {
            supportedLanguages = new List<string>(VOInfo.supportedLanguages);
            currentLanguage = supportedLanguages[0];
        }

        Dictionary<string, Dictionary<string, AudioClip>> tempAudio = new Dictionary<string, Dictionary<string, AudioClip>>();

        // Load Audio Files
        for (int i = 0; i < VOInfo.audioInfo.Length; ++i)
        {
            VOFileLocations.AudioInfo audioInfo = VOInfo.audioInfo[i];
            string language = audioInfo.language;


            // Load Audio Clips
            for (int j = 0; j < audioInfo.audioFolderPaths.Length; ++j)
            {
                AudioClip[] audioClips = Resources.LoadAll<AudioClip>(audioInfo.audioFolderPaths[j]);

                // Add Audio Clips to Dictionary
                for (int k = 0; k < audioClips.Length; ++k)
                {
                    string key = audioClips[k].name;

                    //Debug.Log(key);

                    if (!tempAudio.ContainsKey(key))
                    {
                        tempAudio.Add(key, new Dictionary<string, AudioClip>());
                    }

                    tempAudio[key].Add(language, audioClips[k]);

                    //Debug.Log(tempAudio[key][language].name);
                }
            }
        }

        // Load Text
        for (int i = 0; i < VOInfo.jsonPaths.Length; ++i)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(VOInfo.jsonPaths[i]);

            for (int j = 0; j < textAssets.Length; ++j)
            {
                VOArray.VOLineInfo[] array = JsonUtility.FromJson<VOArray>(textAssets[j].text).array;

                for (int k = 0; k < array.Length; ++k)
                {
                    string language = array[k].language;
                    string text = array[k].text;
                    string key = array[k].audioFile;

                    if (!voiceOverLines.ContainsKey(key))
                    {
                        voiceOverLines.Add(key, new Dictionary<string, VOLine>());
                    }

                    if(voiceOverLines[key].ContainsKey(language))
                    {
                        Debug.LogError("Language (" + language + ") Detected multiple times for Key: " + key);
                    }
                    else
                    {
                        if (!tempAudio.ContainsKey(key))
                        {
                            Debug.LogError("No audio found for given key: " + key);
                        }
                        else
                        {
                            Dictionary<string, AudioClip> temp = tempAudio[key];

                            if (!temp.ContainsKey(language))
                            {
                                Debug.LogError("No audio file found for key (" + key + ") and language (" + language + ")");
                            }
                            else
                            {
                                AudioClip audio = temp[language];

                                if (audio != null)
                                {
                                    VOLine toAdd = new VOLine(audio, text);
                                    voiceOverLines[key].Add(language, toAdd);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public Dictionary<string, VOLine> getVOLine(string key)
    {
        return voiceOverLines[key];
    }
}

#region Utility Classes

[Serializable]
public class VOFileLocations
{
    public string[] jsonPaths;
    public string[] supportedLanguages;
    public AudioInfo[] audioInfo;

    [Serializable]
    public class AudioInfo
    {
        public string language;
        public string[] audioFolderPaths;
    }
}

[Serializable]
public class VOArray
{
    public VOLineInfo[] array;

    [Serializable]
    public class VOLineInfo
    {
        public string audioFile;
        public string language;
        public string text;
    }
}

#endregion

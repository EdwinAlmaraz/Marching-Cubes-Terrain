using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dimmer : MonoBehaviour
{
    private Image dimmer;
    void Awake()
    {
        dimmer = GetComponent<Image>();
    }

    public IEnumerator FadeTo(bool toDark, float fadeTime)
    {
        if (toDark)
        {
            float fromAlphaValue = dimmer.color.a;
            float toAlphaValue = 1f;
            for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / fadeTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(fromAlphaValue, toAlphaValue, t));
                dimmer.color = newColor;

                yield return null;
            }
            dimmer.color = new Color(0, 0, 0, toAlphaValue);
        }
        else
        {
            float fromAlphaValue = dimmer.color.a;
            float toAlphaValue = 0f;
            for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / fadeTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(fromAlphaValue, toAlphaValue, t));
                dimmer.color = newColor;

                yield return null;
            }
            dimmer.color = new Color(0, 0, 0, toAlphaValue);
        }
    }

    public void Fade(bool toDark, float fadeTime)
    {
        StartCoroutine(FadeTo(toDark, fadeTime));
    }


}
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader instance = null;
    public GameObject dimmerObj;
    public Image dimmerScreen;

    private void Awake()
    {
        instance = this;
    }

}

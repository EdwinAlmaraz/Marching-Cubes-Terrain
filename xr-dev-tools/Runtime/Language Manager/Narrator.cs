using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public static Narrator instance {get; private set;} = null;

    public AudioSource audioSource { get; private set; } = null;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
}

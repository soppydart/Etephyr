using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    public float volume;
    public AudioSource audioSource;
}

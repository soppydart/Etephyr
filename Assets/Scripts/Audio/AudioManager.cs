using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    void Awake()
    {
        int n = FindObjectsOfType<AudioManager>().Length;
        if (n > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            // sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = true;
            // sound.audioSource.Play();
        }
    }
    void Update()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource.volume = sound.volume;
        }
    }
    public void PlaySound(string sName)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == sName)
            {
                s.audioSource.Play();
                break;
            }
        }
    }
    public void StopSound(string sName)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == sName)
            {
                s.audioSource.Stop();
                break;
            }
        }
    }
    public void FadeOutSound(string sName)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == sName)
            {
                while (s.volume > 0)
                    s.volume -= Time.deltaTime;
                StopSound(sName);
            }
        }
    }
}

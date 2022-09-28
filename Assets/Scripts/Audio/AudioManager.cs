using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    [SerializeField] AudioMixerGroup audioMixer;
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
            sound.audioSource.outputAudioMixerGroup = audioMixer;
        }
    }
    void Update()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }
    }
    public void PlaySound(string sName)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == sName)
            {
                if (!s.audioSource.isPlaying)
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
    public IEnumerator FadeOutSound(string sName)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == sName)
            {
                // StartFade(&s, 1f, 0f);
                float currentTime = 0;
                float start = s.volume;
                while (currentTime < 5f)
                {
                    currentTime += Time.deltaTime;
                    s.volume = Mathf.Lerp(start, 0, currentTime / 5f);
                    Debug.Log(s.volume);
                }
                yield return new WaitForSeconds(0.1f);
                // s.volume = 0.5f;
            }
        }
    }
    public void StartBossPhaseTransition()
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == "Boss Music Loop")
            {
                while (s.pitch > 0.6f)
                    s.pitch -= Time.deltaTime / 4;
            }
        }
    }
    public void EndBossPhaseTransition()
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == "Boss Music Loop")
            {
                while (s.pitch < 1f)
                    s.pitch += Time.deltaTime / 4;
            }
        }
    }
    // IEnumerator StartFade(Sound* s, float duration, float targetVolume)
    // {
    //     float currentTime = 0;
    //     float start = s.volume;
    //     while (currentTime < duration)
    //     {
    //         currentTime += Time.deltaTime;
    //         s.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
    //         yield return null;
    //     }
    // }
    public void LowerPitch()
    {
        foreach (Sound s in sounds)
        {
            while (s.pitch > 0.4f)
                s.pitch -= Time.deltaTime / 4;
        }
    }
    public void IncreasePitch()
    {
        foreach (Sound s in sounds)
        {
            while (s.pitch < 1f)
                s.pitch += Time.deltaTime / 4;
        }
    }
    public void DestroyAudioManager()
    {
        Destroy(gameObject);
    }
}

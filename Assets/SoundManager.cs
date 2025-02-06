using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioClip nitroSfx;
    public AudioClip shotSfx;
    public AudioClip carSfx;
    public AudioClip carCrashSfx;

    private Dictionary<AudioClip, AudioSource> audioSources = new Dictionary<AudioClip, AudioSource>();

    private void OnEnable()
    {
        EventManager.Instance.OnNitroStateChanged += HandleNitroSound;
        EventManager.Instance.OnShotFired += () => PlaySound(shotSfx);
        EventManager.Instance.OnCarMoved += () => PlaySound(carSfx);
        EventManager.Instance.OnCarStopped += () => StopSound(carSfx);
        EventManager.Instance.OnCarCrashed += () => PlaySound(carCrashSfx);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNitroStateChanged -= HandleNitroSound;
    }

    private void HandleNitroSound(bool isActive)
    {
        if (isActive)
            PlaySound(nitroSfx);
        else
            StopSound(nitroSfx);
    }

    private void PlaySound(AudioClip clip)
    {
        // debug log
        Debug.Log("Playing sound: " + clip.name);
        if (!audioSources.ContainsKey(clip))
        {
            Debug.Log("Creating new audio source");
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = clip;
            if (clip == carSfx) {
                newAudioSource.loop = true;
            }
            newAudioSource.Play();
            audioSources[clip] = newAudioSource;
        }
        else
        {
            Debug.Log("Audio source already exists");
            audioSources[clip].Play();
        }
    }

    private void StopSound(AudioClip clip)
    {
        if (audioSources.ContainsKey(clip))
        {
            audioSources[clip].Stop();
        }
    }
}
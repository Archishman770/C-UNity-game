using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;

    [Header("Sound Libraries")]
    public SoundLibrary playerSounds;
    public SoundLibrary bossSounds;
    public SoundLibrary uiSounds;
    public SoundLibrary environmentSounds;

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeSoundDictionary();
    }

    void InitializeSoundDictionary()
    {
        // Player sounds
        foreach (var sound in playerSounds.sounds)
        {
            soundDictionary.Add(sound.name, sound.clip);
        }

        // Boss sounds
        foreach (var sound in bossSounds.sounds)
        {
            soundDictionary.Add(sound.name, sound.clip);
        }

        // UI sounds
        foreach (var sound in uiSounds.sounds)
        {
            soundDictionary.Add(sound.name, sound.clip);
        }

        // Environment sounds
        foreach (var sound in environmentSounds.sounds)
        {
            soundDictionary.Add(sound.name, sound.clip);
        }
    }

    public void PlaySound(string soundName, float volume = 1f, bool loop = false)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            AudioSource source = sfxSource;
            if (loop) 
            {
                source = ambientSource;
                source.loop = true;
            }

            source.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"Sound not found: {soundName}");
        }
    }

    public void PlayMusic(string musicName, float volume = 1f)
    {
        if (soundDictionary.TryGetValue(musicName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}

[System.Serializable]
public class SoundLibrary
{
    public string libraryName;
    public List<Sound> sounds;
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float defaultVolume = 1f;
}
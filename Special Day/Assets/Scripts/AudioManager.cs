using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;
        
        [HideInInspector]
        public AudioSource source;
    }
    
    public Sound[] sounds;
    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
    
    private void Awake()
    {
        // Create AudioSource components for each sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            
            soundDictionary[sound.name] = sound;
        }
    }
    
    public void PlaySound(string name)
    {
        if (soundDictionary.TryGetValue(name, out Sound sound))
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found in AudioManager!");
        }
    }
    
    public void StopSound(string name)
    {
        if (soundDictionary.TryGetValue(name, out Sound sound))
        {
            sound.source.Stop();
        }
    }
    
    public void SetVolume(string name, float volume)
    {
        if (soundDictionary.TryGetValue(name, out Sound sound))
        {
            sound.source.volume = Mathf.Clamp01(volume);
        }
    }
    
    public bool IsPlaying(string name)
    {
        return soundDictionary.TryGetValue(name, out Sound sound) && sound.source.isPlaying;
    }
}
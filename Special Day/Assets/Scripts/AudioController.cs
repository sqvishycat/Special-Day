using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isPreloaded = false;
    void Awake()
    {
        // If AudioSource wasn't assigned in inspector, try to get it
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        PreloadAudio();
    }
    private void PreloadAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.clip.LoadAudioData();
            isPreloaded = true;
            Debug.Log("Audio preloaded successfully");
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing");
        }
    }
    public void PlayAudio()
    {
        if (!isPreloaded)
        {
            PreloadAudio();
        }
        if (audioSource != null && isPreloaded)
        {
            audioSource.Play();
            Debug.Log("Playing audio");
        }
    }
    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    // Optional: Call this when the scene is being unloaded
    void OnDestroy()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.clip.UnloadAudioData();
        }
    }
}
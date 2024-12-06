using UnityEngine;
using System.Collections;

public class AudioFadeController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeInDuration = 2f;
    [SerializeField] private float targetVolume = 1f;
    
    private void Start()
    {
        // Ensure the audio source is set up correctly
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        // Configure the AudioSource
        audioSource.loop = true;
        audioSource.volume = 0f;
        
        // Start playing and fading in
        audioSource.Play();
        StartCoroutine(FadeIn());
    }
    
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(0f, targetVolume, elapsedTime / fadeInDuration);
            audioSource.volume = newVolume;
            yield return null;
        }
        
        // Ensure we end up exactly at the target volume
        audioSource.volume = targetVolume;
    }
}
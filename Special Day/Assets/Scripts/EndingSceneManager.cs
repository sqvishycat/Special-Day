using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    private AudioController audioController;

    void Start()
    {
        // Get the AudioController component from the same GameObject
        audioController = GetComponent<AudioController>();
        
        // Play the audio when the scene starts
        audioController.PlayAudio();
    }
}
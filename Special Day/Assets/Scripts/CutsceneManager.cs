using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioController audioController;
    [SerializeField] private PlayableDirector playableDirector;  // For your animation timeline
    [SerializeField] private GameObject playerObject;  // Reference to your player
    
    [Header("Settings")]
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool disablePlayerDuringCutscene = true;

    private void Start()
    {
        if (playOnStart)
        {
            StartCutscene();
        }
    }

    public void StartCutscene()
    {
        // Disable player control if needed
        if (disablePlayerDuringCutscene && playerObject != null)
        {
            DisablePlayerControl();
        }

        // Start animation if we have a playable director
        if (playableDirector != null)
        {
            playableDirector.Play();
        }

        // Start audio
        if (audioController != null)
        {
            audioController.PlayAudio();
        }
    }

    public void EndCutscene()
    {
        // Re-enable player control
        if (disablePlayerDuringCutscene && playerObject != null)
        {
            EnablePlayerControl();
        }

        // Stop animation
        if (playableDirector != null)
        {
            playableDirector.Stop();
        }

        // Stop audio
        if (audioController != null)
        {
            audioController.StopAudio();
        }
    }

    private void DisablePlayerControl()
    {
        // Disable player movement script
        var playerMovement = playerObject.GetComponent<PlayerMovement>();  // Replace with your actual movement script name
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable other player components as needed
        // Example:
        // playerObject.GetComponent<PlayerInput>().enabled = false;
    }

    private void EnablePlayerControl()
    {
        // Re-enable player movement script
        var playerMovement = playerObject.GetComponent<PlayerMovement>();  // Replace with your actual movement script name
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Re-enable other player components as needed
        // Example:
        // playerObject.GetComponent<PlayerInput>().enabled = true;
    }

    // Optional: Method to skip cutscene
    public void SkipCutscene()
    {
        EndCutscene();
    }
}
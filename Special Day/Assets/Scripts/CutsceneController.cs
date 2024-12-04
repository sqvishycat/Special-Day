using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject playerObject; // Optional: to disable player during cutscene

    [Header("Debug")]
    [SerializeField] private bool showDebugLogs = true;

    private bool isPlaying = false;

    void Awake()
    {
        // Ensure components aren't null
        if (timeline == null) timeline = GetComponent<PlayableDirector>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        // Disable auto-play
        if (timeline != null) timeline.playOnAwake = false;
        if (audioSource != null) 
        {
            audioSource.playOnAwake = false;
            audioSource.Stop(); // Ensure audio isn't playing
        }
    }

    void Start()
    {
        StartCutscene();
    }

    public void StartCutscene()
    {
        StartCoroutine(BeginCutsceneSequence());
    }

    private IEnumerator BeginCutsceneSequence()
    {
        if (showDebugLogs) Debug.Log("Starting cutscene sequence");

        // Disable player if needed
        if (playerObject != null)
            playerObject.SetActive(false);

        // Wait for everything to be ready
        yield return new WaitForEndOfFrame();

        // Reset both timeline and audio to start
        if (timeline != null)
        {
            timeline.time = 0;
            timeline.Evaluate();
            timeline.Stop();
        }

        if (audioSource != null)
        {
            audioSource.time = 0;
            audioSource.Stop();
        }

        // Small delay to ensure everything is ready
        yield return new WaitForSeconds(0.1f);

        // Start both
        if (timeline != null)
        {
            if (showDebugLogs) Debug.Log("Starting timeline");
            timeline.Play();
        }

        if (audioSource != null)
        {
            if (showDebugLogs) Debug.Log("Starting audio");
            audioSource.Play();
        }

        isPlaying = true;

        // Wait for cutscene to finish
        if (audioSource != null && audioSource.clip != null)
        {
            float duration = audioSource.clip.length;
            yield return new WaitForSeconds(duration);
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        if (showDebugLogs) Debug.Log("Ending cutscene");
        isPlaying = false;

        // Re-enable player if needed
        if (playerObject != null)
            playerObject.SetActive(true);

        // Any other cleanup needed
    }

    // Optional: Add method to skip cutscene
    public void SkipCutscene()
    {
        if (!isPlaying) return;

        StopAllCoroutines();
        if (timeline != null) timeline.Stop();
        if (audioSource != null) audioSource.Stop();
        EndCutscene();
    }
}
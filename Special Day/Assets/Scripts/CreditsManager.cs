using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private float endPosition; // The Y position where credits end
    private RectTransform textTransform;

    void Start()
    {
        textTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Check if credits have reached their final position
        if (textTransform.anchoredPosition.y >= endPosition)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
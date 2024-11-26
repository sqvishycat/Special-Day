using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 10f;  // Speed of player movement
    public float cameraRotationSpeed = 50f;  // Speed of camera rotation

    private Transform cameraTransform;

    void Start()
    {
        // Reference the main camera in the scene
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    void HandleMovement()
    {
        // Move the player forward in the direction they are facing when 'W' is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // Use transform.forward to move in the direction the player is facing
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void HandleCameraRotation()
    {
        // Rotate the camera left when 'A' is pressed
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -cameraRotationSpeed * Time.deltaTime);
        }

        // Rotate the camera right when 'D' is pressed
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, cameraRotationSpeed * Time.deltaTime);
        }
    }
}
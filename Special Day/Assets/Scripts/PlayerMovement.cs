using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb; // Rigidbody for the player
    public Transform head; // Player head/camera
    public Camera camera;

    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed of movement
    public float rotationSpeed = 100f; // Speed of rotation

    private Vector3 movementInput; // Stores forward movement input

    private void Update()
    {
        HandleRotation(); // Handles A/D rotation
        HandleMovementInput(); // Collects W forward input
    }

    private void FixedUpdate() // For physics updates
    {
        ApplyMovement(); // Applies velocity to Rigidbody
    }

    private void HandleMovementInput()
    {
        movementInput = Vector3.zero;

        // Move forward in the direction the player is facing (W key)
        if (Input.GetKey(KeyCode.W))
        {
            movementInput = transform.forward * moveSpeed;
        }
    }

    private void ApplyMovement()
    {
        // Apply velocity based on forward input
        rb.linearVelocity = new Vector3(movementInput.x, rb.linearVelocity.y, movementInput.z); // Preserve vertical velocity
    }

    private void HandleRotation()
    {
        // Rotate left/right (A/D keys)
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Rotate right
        }

        // Apply rotation to the player object
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
}

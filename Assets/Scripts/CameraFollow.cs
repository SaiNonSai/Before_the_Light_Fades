using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign the Player in the Inspector
    public float smoothSpeed = 5f; // Higher = faster camera
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 2D camera default

    public float mouseOffsetStrength = 2f;
    public float maxMouseOffset = 3f; // Clamp the max displacement

    void LateUpdate()
    {
        if (target == null) return;

        // Use Input System to get mouse position
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // Direction vector from player to mouse
        Vector3 directionToMouse = (mouseWorldPos - target.position).normalized;

        // Mouse offset scaled by strength
        Vector3 mouseOffset = directionToMouse * mouseOffsetStrength;

        // Optional: Clamp offset so it's not too far
        if (mouseOffset.magnitude > maxMouseOffset)
            mouseOffset = mouseOffset.normalized * maxMouseOffset;

        // Final camera target position
        Vector3 desiredPosition = target.position + offset + mouseOffset;
        desiredPosition.z = offset.z;

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

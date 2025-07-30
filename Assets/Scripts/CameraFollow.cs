using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign player
    public float smoothSpeed = 10f;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // z = -10 for 2D camera

    public float mouseOffsetStrength = 2f;
    public float maxMouseOffset = 3f;

    void LateUpdate()
    {
        if (target == null || Camera.main == null) return;

        // Get mouse position in world space
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // Direction vector from player to mouse
        Vector3 displacement = (mouseWorldPos - target.position).normalized * mouseOffsetStrength;

        // Clamp the displacement vector to prevent extreme offset
        displacement = Vector3.ClampMagnitude(displacement, maxMouseOffset);

        // Final target position = player center + offset + mouse pull
        Vector3 desiredPosition = target.position + offset + displacement;
        desiredPosition.z = offset.z; // ensure correct z

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

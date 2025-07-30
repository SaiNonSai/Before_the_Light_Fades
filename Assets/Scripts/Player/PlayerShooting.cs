using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.2f;

    private float fireTimer;
    private bool isShooting;
    private PlayerControls controls;

    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Movement.Fire.performed += ctx => isShooting = true;
        controls.Movement.Fire.canceled += ctx => isShooting = false;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        fireTimer += Time.deltaTime;

        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        // Flip player sprite left/right
        spriteRenderer.flipX = mousePos.x < transform.position.x;

        // Fire bullet
        if (isShooting && fireTimer >= fireCooldown)
        {
            Vector2 direction = (mousePos - firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(direction);

            fireTimer = 0f;
        }
    }

}

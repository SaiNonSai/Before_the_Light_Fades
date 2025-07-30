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

    //upgrades
    public int bulletDamage = 10;
    public bool isMultishotEnabled = false;


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

            if (isMultishotEnabled)
            {
                FireBullet(direction);
                FireBullet(Quaternion.Euler(0, 0, 15) * direction);
                FireBullet(Quaternion.Euler(0, 0, -15) * direction);
            }
            else
            {
                FireBullet(direction);
            }

            fireTimer = 0f;
        }

    }

    void FireBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(direction);
        bulletScript.damage = bulletDamage;
    }

    public void IncreaseBulletDamage(int amount)
    {
        bulletDamage += amount;
    }

    public void EnableMultishot()
    {
        isMultishotEnabled = true;
    }

}

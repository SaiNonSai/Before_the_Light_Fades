using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using TMPro;

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

    //bullets
    public int magazineSize = 20;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = magazineSize;
    }

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
        if (isShooting && fireTimer >= fireCooldown && !isReloading && currentAmmo > 0)
        {
            Vector2 direction = (mousePos - firePoint.position).normalized;

            if (isMultishotEnabled)
            {
                FireBullet(direction);
                FireBullet(Quaternion.Euler(0, 0, 15) * direction);
                FireBullet(Quaternion.Euler(0, 0, -15) * direction);
                currentAmmo -= 3;
            }
            else
            {
                FireBullet(direction);
                currentAmmo--;
            }

            fireTimer = 0f;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }

        if (ammoText != null)
        {
            ammoText.text = isReloading ? "Reloading..." : $"{currentAmmo} / {magazineSize}";
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
        Debug.Log("Reload complete.");
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

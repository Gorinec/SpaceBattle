
using TMPro;
using UnityEngine;

public class ShipWeaponSystem : MonoBehaviour
{
    [Header("Стрельба")]
    public GameObject bulletPrefab;
    public Transform leftGun;
    public Transform rightGun;
    public float bulletSpeed = 30f;
    public float fireRate = 0.2f;

    [Header("Патроны")]
    public int maxAmmo = 100;
    public int currentAmmo = 100;
    public float ammoRechargeRate = 3f;
    public float rechargeDelay = 1f;

    [Header("UI")]
    public TMP_Text ammoText;

    [Header("Звуки")] 
    public AudioClip shootSound; 
    public AudioSource audioSource; 

    private float nextFireTime;
    private float lastFireTime;

    void Start()
    {
        UpdateAmmoUI();

        
    }

    void Update()
    {
        // Стрельба
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.isPressed &&
            Time.time >= nextFireTime &&
            currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
            lastFireTime = Time.time;
        }

        // Восстановление патронов
        if (Time.time - lastFireTime >= rechargeDelay && currentAmmo < maxAmmo)
        {
            RechargeAmmo();
        }

        // Обновляем UI
        if (Time.frameCount % 10 == 0)
        {
            UpdateAmmoUI();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        // Проигрываем звук выстрела
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Тратим 1 патрон
        currentAmmo--;
        currentAmmo = Mathf.Max(currentAmmo, 0);

        Rigidbody shipRb = GetComponent<Rigidbody>();
        Vector3 shipVelocity = shipRb != null ? shipRb.linearVelocity : Vector3.zero;

        // Левая пушка
        if (leftGun != null)
        {
            Vector3 firePos = leftGun.position + leftGun.forward * 1f;
            GameObject bullet1 = Instantiate(bulletPrefab, firePos, leftGun.rotation);
            bullet1.transform.parent = null;

            Rigidbody rb1 = bullet1.GetComponent<Rigidbody>();
            if (rb1 != null)
            {
                rb1.linearVelocity = leftGun.forward * bulletSpeed + shipVelocity;
            }

            Destroy(bullet1, 3f);
        }

        // Правая пушка
        if (rightGun != null)
        {
            Vector3 firePos = rightGun.position + rightGun.forward * 1f;
            GameObject bullet2 = Instantiate(bulletPrefab, firePos, rightGun.rotation);
            bullet2.transform.parent = null;

            Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
            if (rb2 != null)
            {
                rb2.linearVelocity = rightGun.forward * bulletSpeed + shipVelocity;
            }

            Destroy(bullet2, 3f);
        }
    }

    void RechargeAmmo()
    {
        float ammoToAdd = ammoRechargeRate * Time.deltaTime;
        currentAmmo = Mathf.Min(currentAmmo + Mathf.CeilToInt(ammoToAdd), maxAmmo);
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Патроны: {currentAmmo}/{maxAmmo}";

            if (currentAmmo < 10)
                ammoText.color = Color.red;
            else if (currentAmmo < 15)
                ammoText.color = Color.yellow;
            else
                ammoText.color = Color.white;
        }

    }
}
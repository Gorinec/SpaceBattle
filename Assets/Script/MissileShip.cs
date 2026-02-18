using UnityEngine;

public class MissileShip : MonoBehaviour
{
    [Header("Преследование")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float stopDistance = 5f;

    [Header("Стрельба")]
    public GameObject rocketPrefab;
    public Transform firePoint;
    public float fireDistance = 10f;
    public float fireRate = 0.5f;
    public float rocketSpeed = 20f;

    [Header("Здоровье")]
    public float health = 100f;

    [Header("Эффекты")]
    public GameObject deathEffect; 

    private Transform player;
    private float nextShotTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (distanceToPlayer <= fireDistance && Time.time >= nextShotTime)
        {
            ShootAtPlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        
        Destroy(gameObject);
    }

    void ShootAtPlayer()
    {
        if (rocketPrefab == null || firePoint == null) return;

        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        Quaternion rocketRotation = Quaternion.LookRotation(directionToPlayer);

        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, rocketRotation);

        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = directionToPlayer * rocketSpeed;
        }

        Destroy(rocket, 5f);
        nextShotTime = Time.time + 1f / fireRate;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
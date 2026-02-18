using UnityEngine;

public class RamEnemy : MonoBehaviour
{
    [Header("Преследование")]
    public float speed = 5f;
    public float damage = 20f;
    public float rotationSpeed = 3f;

    [Header("Здоровье")]
    [SerializeField]
    private float health = 100f;

    [Header("Эффекты")]
    public GameObject deathEffect; 

    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        FindPlayer();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;
        }

        rb.isKinematic = false;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
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

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
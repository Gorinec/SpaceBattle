using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 1f;
    public float lifetime = 3f;

    void Start()
    {
        

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Движение вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем попали ли в игрока
        if (other.CompareTag("Player"))
        {
            // Наносим урон
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            

            // Уничтожаем снаряд
            Destroy(gameObject);
        }
    }

}
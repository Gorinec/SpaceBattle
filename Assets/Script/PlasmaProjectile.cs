using UnityEngine;

public class PlasmaProjectile : MonoBehaviour
{
    public float speed = 40f;
    public float damage = 10f;

    private LineRenderer line;
    private Vector3 lastPos;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Создаём LineRenderer
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 1f;
        line.endWidth = 0.1f;
        line.positionCount = 2;

        
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.material.color = Color.white;

        
        line.startColor = Color.white;
        line.endColor = new Color(1, 1, 1, 0);

        lastPos = transform.position;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        
        if (rb != null)
        {
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        // Рисуем линию от текущей позиции к предыдущей
        line.SetPosition(0, transform.position);
        line.SetPosition(1, lastPos);

        lastPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Gun"))
            return;

        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }

}
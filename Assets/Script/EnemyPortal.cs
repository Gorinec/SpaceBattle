
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public float radius = 450f;
    public float interval = 2f;

    void Start()
    {
        InvokeRepeating("Spawn", 1f, interval);
    }

    void Spawn()
    {
        // Выбираем случайного врага
        GameObject enemy;
        if (Random.Range(0, 2) == 0)
            enemy = enemy1;
        else
            enemy = enemy2;

        // Случайная позиция по кругу
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );

        // Создаём врага
        Instantiate(enemy, pos, Quaternion.identity);
    }
}
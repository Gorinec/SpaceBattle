using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height = 15f;
    public float distance = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Камера сверху
        transform.position = target.position + new Vector3(0, height, -distance);

        // Смотрим на корабль
        transform.LookAt(target);
    }
}
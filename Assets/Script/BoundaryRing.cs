using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float radius = 500f;
    public int segments = 64;
    public float lineWidth = 5f;

    void Start()
    {
        LineRenderer lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = segments + 1;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.loop = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = Color.red;
        lr.startColor = Color.red;
        lr.endColor = Color.red;


        // Рисуем круг
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * (360f / segments) * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );
            lr.SetPosition(i, pos);

        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float arrowLength;

    private void Awake()
    {
        arrowLength = spriteRenderer.bounds.size.x;
    }
    public void SpawnRandomArrow(Action<List<Vector2>> onReady)
    {
        
        float angle = UnityEngine.Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

       
        List<Vector2> standard = ArrowPoints(10);

        
        onReady?.Invoke(standard);
    }

    public List<Vector2> ArrowPoints(int count = 10)
    {

        Vector2 center = transform.position;
        Vector2 dir = transform.right.normalized;
        Vector2 start = center - dir * (arrowLength / 2f);
        Vector2 end = center + dir * (arrowLength / 2f);

        List<Vector2> points = new();
        for (int i = 0; i < count; i++)
        {
            float t = i / (float)(count - 1);
            points.Add(Vector2.Lerp(start, end, t));
        }

        return points;
    }
}

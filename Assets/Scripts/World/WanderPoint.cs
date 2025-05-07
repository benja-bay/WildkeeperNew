using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WanderPoint : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private int  _maxAttempts = 5;
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetRandomPoint()
    {
        var bounds = _boxCollider2D.bounds;
        Vector2 point = Vector2.zero;
        
        var attempts = 0;

        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            point = new Vector2(x, y);
            attempts++;
        } while (attempts < _maxAttempts && _boxCollider2D.OverlapPoint(point));
        
        return point;
    }
}

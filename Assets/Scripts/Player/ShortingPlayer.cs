using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortingPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}

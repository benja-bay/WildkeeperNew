using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSorting : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Cuanto menor es Y, mayor el order (se ve por delante)
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}

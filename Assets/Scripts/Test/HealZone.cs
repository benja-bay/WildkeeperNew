using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player.PlayerHealth playerHealth))
        {
            playerHealth.Regenerate(healAmount);
        }
    }
}
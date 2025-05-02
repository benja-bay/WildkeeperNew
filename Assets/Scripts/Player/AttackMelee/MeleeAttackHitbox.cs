using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    public int damage = 1;
    // Cuando entre en contacto con un game object debe obtener su componente de vida y aplicar una cantidad de daño variable
    private void Start()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        if (inputHandler == null)
        {
            Debug.LogWarning("PlayerInputHandler no encontrado en el objeto padre.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null && inputHandler.attackPressed)
        {
            enemyHealth.TakeDamage(damage);
            Debug.Log("Damage was caused to the enemy");
        }
        else
        {
            Debug.Log("Enemy Health is null");
        }
    }
}

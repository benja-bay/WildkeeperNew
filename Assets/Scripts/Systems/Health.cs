// ==============================
// Health.cs
// Base class for managing health, damage, healing, and death behavior
// ==============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // === Health Stats ===
    [Header("Stats")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;
    protected bool Alive = true;

    // === Initialize Health on Awake ===
    public void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    // === Handles Taking Damage and Triggers Death if Health Drops to Zero ===
    public virtual void TakeDamage(int amount)
    {
        if(amount < 0)
        {
            Debug.LogError("Damage taken cannot be less than 0.");
            return;
        }

        if(_currentHealth <= 0)
        {
            Die();
        }

        if (Alive)
        {
            _currentHealth -= amount;
            Debug.Log($"{gameObject.name} recibió {amount} de daño.");
        }
        Debug.Log($"Salud actual: {_currentHealth}");
    }

    // === Virtual Death Logic ===
    public virtual void Die() {
        Debug.Log($"{gameObject.name} has died.");
        Alive = false;
        // TODO: Die logic.
    }

    // === Handles Healing and Caps Health at Max ===
    public virtual void Heal(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("Healing amount must be greater than 0.");
            return;
        }

        if(_currentHealth < _maxHealth && Alive)
        {
            _currentHealth += amount;
            Debug.Log($"{gameObject.name} regenero {amount} de salud. Salud actual: {_currentHealth}");

            if(_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        else
        {
            if (Alive)
            {
                Debug.Log($"{gameObject.name} already has full health.");
            }
            else
            {
                Debug.Log($"{gameObject.name} cannot regenerate because he is dead.");
            }
        }
    }

    // === Health Accessors ===
    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
}
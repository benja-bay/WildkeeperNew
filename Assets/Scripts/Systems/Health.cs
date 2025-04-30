using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;

    public void Awake() {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount) {
        if(amount < 0) {
            Debug.LogError("Damage taken cannot be less than 0.");
            return;
        }

        _currentHealth -= amount;

        if(_currentHealth <= 0) {
            Die();
        }
    }
    public virtual void Die() {
        Debug.Log($"{gameObject.name} has died.");
        // TODO: Die logic.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;

    public void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int amount)  // metodo virtual para sobreescribirlo
    {
        if(amount < 0) // si el daño es negativo
        {
            Debug.LogError("Damage taken cannot be less than 0.");
            return;// finaliza
        }

        _currentHealth -= amount; // se resta salud
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Salud actual: {_currentHealth}");

        if(_currentHealth <= 0) // si la vida actual es menor a 0
        {
            Die(); // se muere :c
        }
    }
    public virtual void Die() {
        Debug.Log($"{gameObject.name} has died.");
        // TODO: Die logic.
    }
    
    public virtual void Heal(int amount) // metodo para curarse
    {
        if (amount <= 0) // si la regeneracion es negativa
        {
            Debug.LogError("Healing amount must be greater than 0.");
            return;// finaliza
        }

        if(_currentHealth < _maxHealth) { // si la vida actual es menor que la vida maxima 

            _currentHealth += amount; // se suma la curacion
            
            if(_currentHealth > _maxHealth) { // si la vida actual supera la maxima
                _currentHealth = _maxHealth; // ajusta para no superar limite
            }
        }
        else // si ya tiene toda la vida
        {
            Debug.Log($"{gameObject.name} already has full health.");
        }
    }
    
    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
}

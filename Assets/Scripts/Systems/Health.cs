// ==============================
// Health.cs
// Base class for managing health, damage, healing, and death behavior
// ==============================

using UnityEngine;

namespace Systems
{
    public class Health : MonoBehaviour
    {
        // === Health Stats ===
        [Header("Stats")]
        [SerializeField] protected int _maxHealth = 100; // Maximum health value
        protected int _currentHealth; // Current health
        protected bool Alive = true; // Alive status

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
                Debug.Log($"{gameObject.name} took {amount} damage.");
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
                Debug.Log($"{gameObject.name} healed {amount} HP. Current health: {_currentHealth}");

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
}
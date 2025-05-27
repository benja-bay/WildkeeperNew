using UnityEngine;

namespace Enemies
{
    // Sistema de ataque melee del enemigo
    public class EnemyMeleeHitbox : MonoBehaviour
    {
        public int DamageAmount { get; set; }
        public float DamageCooldown { get; set; }
        public float AttackDistance { get; set; }

        private float _lastDamageTime = float.MinValue;
        private Transform _playerTransform;
        private Player.PlayerHealth _playerHealth;
        private bool _isPlayerInRange;

        private void Update()
        {
            // Inicializa referencias al jugador si aún no existen
            if (_playerTransform == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    _playerTransform = player.transform;
                    _playerHealth = player.GetComponent<Player.PlayerHealth>();
                }
            }

            // Coloca el hitbox frente al enemigo en dirección al jugador
            if (_playerTransform != null)
            {
                Vector3 direction = (_playerTransform.position - transform.parent.position).normalized;
                transform.position = transform.parent.position + direction * AttackDistance;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            // Si el jugador está en rango y el cooldown terminó, aplicar daño
            if (_isPlayerInRange && Time.time - _lastDamageTime >= DamageCooldown)
            {
                ApplyDamage();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                if (_playerHealth == null)
                    _playerHealth = other.GetComponent<Player.PlayerHealth>();

                if (Time.time - _lastDamageTime >= DamageCooldown)
                {
                    ApplyDamage();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
            }
        }

        private void ApplyDamage()
        {
            // Aplica daño al jugador
            if (_playerHealth != null)
            {
                _playerHealth.TakeDamage(DamageAmount);
                _lastDamageTime = Time.time;
            }
        }
    }
}

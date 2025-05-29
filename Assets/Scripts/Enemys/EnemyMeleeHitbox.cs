// #TEST
using UnityEngine;

namespace Enemys
{
    public class EnemyMeleeHitbox : MonoBehaviour
    {
        [Header("Ataque")]
        [SerializeField] private int _damageAmount = 10;
        [SerializeField] private float _damageCooldown = 1f;
        private float _lastDamageTime;

        [Header("Orientación al Jugador")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _attackDistance = 1.5f; // Distancia del hitbox respecto al enemigo

        [Header("Visualización")]
        [SerializeField] private Color _gizmoColor = Color.red;

        private void Update()
        {
            // Buscar al jugador si no está asignado
            if (_playerTransform == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    _playerTransform = player.transform;
            }

            if (_playerTransform != null)
            {
                // Dirección al jugador
                Vector3 direction = (_playerTransform.position - transform.parent.position).normalized;

                // Posicionar el hitbox a una distancia fija desde el enemigo
                transform.position = transform.parent.position + direction * _attackDistance;

                // Rotar el hitbox para que mire al jugador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (Time.time - _lastDamageTime >= _damageCooldown)
            {
                var playerHealth = other.GetComponent<Player.PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damageAmount);
                    Debug.Log("El enemigo le hizo daño al jugador.");
                    _lastDamageTime = Time.time;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;

            // Dibujar la forma del collider (si existe)
            var circle = GetComponent<CircleCollider2D>();
            if (circle != null)
            {
                Vector3 pos = transform.position + (Vector3)circle.offset;
                Gizmos.DrawWireSphere(pos, circle.radius);
            }

            var box = GetComponent<BoxCollider2D>();
            if (box != null)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(box.offset, box.size);
            }
        }
    }
}

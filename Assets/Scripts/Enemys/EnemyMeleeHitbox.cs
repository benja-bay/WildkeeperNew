using UnityEngine;

namespace Enemys
{
    public class EnemyMeleeHitbox : MonoBehaviour
    {
        [field: Header("Configurado por EnemyController")]
        public int DamageAmount { get; set; }
        public float DamageCooldown { get; set; }
        public float AttackDistance { get; set; }

        private float _lastDamageTime;
        private Transform _playerTransform;

        public void Configure(int damage, float cooldown, float distance)
        {
            DamageAmount = damage;
            DamageCooldown = cooldown;
            AttackDistance = distance;
        }

        private void Update()
        {
            if (_playerTransform == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    _playerTransform = player.transform;
            }

            if (_playerTransform != null)
            {
                Vector3 direction = (_playerTransform.position - transform.parent.position).normalized;
                transform.position = transform.parent.position + direction * AttackDistance;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (Time.time - _lastDamageTime >= DamageCooldown)
            {
                var playerHealth = other.GetComponent<Player.PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(DamageAmount);
                    _lastDamageTime = Time.time;
                }
            }
        }
    }
}

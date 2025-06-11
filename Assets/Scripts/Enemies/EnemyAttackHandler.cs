using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyController))]
    public class EnemyAttackHandler : MonoBehaviour
    {
        private EnemyController _enemy;
        private float _cooldownTimer;
        private bool _isMeleeActive;

        private void Awake()
        {
            _enemy = GetComponent<EnemyController>();
        }

        private void Update()
        {
            if (_enemy.Target == null) return;

            float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
            if (distance > _enemy.AttackDistance)
            {
                DeactivateMelee();
                return;
            }

            _cooldownTimer += Time.deltaTime;

            if (_enemy.PrimaryAttackType == AttackType.Range)
            {
                if (_cooldownTimer >= _enemy.DamageCooldown)
                {
                    FireProjectile();
                    _cooldownTimer = 0f;
                }
            }
            else if (_enemy.PrimaryAttackType == AttackType.Melee)
            {
                if (_cooldownTimer >= _enemy.DamageCooldown)
                {
                    ActivateMelee();
                    _cooldownTimer = 0f;
                }
            }
        }

        private void FireProjectile()
        {
            if (_enemy.ProjectilePrefab == null || _enemy.Target == null) return;

            Vector2 direction = (_enemy.Target.position - _enemy.transform.position).normalized;
            if (direction == Vector2.zero) return;

            Vector3 spawnPos = _enemy.transform.position + (Vector3)direction * 0.5f;

            GameObject proj = Instantiate(_enemy.ProjectilePrefab, spawnPos, Quaternion.identity);
            var projectile = proj.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.SetDirection(direction);
                projectile.Speed = _enemy.ProjectileSpeed;
                projectile.Damage = _enemy.DamageAmount;

                Collider2D projCol = proj.GetComponent<Collider2D>();
                Collider2D enemyCol = _enemy.GetComponent<Collider2D>();
                if (projCol != null && enemyCol != null)
                    Physics2D.IgnoreCollision(projCol, enemyCol);
            }
        }

        private void ActivateMelee()
        {
            if (_enemy.MeleeHitbox != null && !_isMeleeActive)
            {
                _enemy.MeleeHitbox.gameObject.SetActive(true);
                _isMeleeActive = true;
                Invoke(nameof(DeactivateMelee), 0.2f); // Ventana corta de ataque
            }
        }

        private void DeactivateMelee()
        {
            if (_enemy.MeleeHitbox != null && _isMeleeActive)
            {
                _enemy.MeleeHitbox.gameObject.SetActive(false);
                _isMeleeActive = false;
            }
        }
    }
}
using UnityEngine;

namespace Enemies.State
{
    public class AttackState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private float _attackCooldownTimer;

        public AttackState(EnemyController enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} entered Attack State.");
            _attackCooldownTimer = 0f;

            if (_enemy.PrimaryAttackType == AttackType.Melee)
            {
                _enemy.ActivateHitbox(true);
            }
        }

        public void Update()
        {
            if (_enemy.Target == null)
            {
                _enemy.SetInitialState();
                return;
            }

            float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);

            if (distance > _enemy.AttackDistance)
            {
                _enemy.SetVisionState();
                return;
            }

            if (_enemy.PrimaryAttackType == AttackType.Range)
            {
                _attackCooldownTimer += Time.deltaTime;
                if (_attackCooldownTimer >= _enemy.DamageCooldown)
                {
                    FireProjectile(_enemy.Target.position);
                    _attackCooldownTimer = 0f;
                }
            }

            // Melee: daño controlado por hitbox
        }

        public void Exit()
        {
            if (_enemy.PrimaryAttackType == AttackType.Melee)
            {
                _enemy.ActivateHitbox(false);
            }

            Debug.Log($"{_enemy.name} exited Attack State.");
        }

        private void FireProjectile(Vector3 targetPosition)
        {
            if (_enemy.ProjectilePrefab == null) return;

            Vector2 direction = (targetPosition - _enemy.transform.position).normalized;
            if (direction == Vector2.zero) return;

            Vector3 spawnPos = _enemy.transform.position + (Vector3)direction * 0.5f;

            GameObject proj = GameObject.Instantiate(_enemy.ProjectilePrefab, spawnPos, Quaternion.identity);

            var projectile = proj.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.SetDirection(direction);
                projectile.Speed = _enemy.ProjectileSpeed;
                projectile.Damage = _enemy.DamageAmount;

                Collider2D projCol = proj.GetComponent<Collider2D>();
                Collider2D enemyCol = _enemy.GetComponent<Collider2D>();
                if (projCol != null && enemyCol != null)
                {
                    Physics2D.IgnoreCollision(projCol, enemyCol);
                }
            }
        }
    }
}
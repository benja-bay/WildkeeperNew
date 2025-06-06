using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyAttackState : IEnemyState
    {
        private EnemyController _controller;
        private NavMeshAgent _agent;
        private float _lastShotTime;

        public EnemyAttackState(EnemyController controller, NavMeshAgent agent)
        {
            _controller = controller;
            _agent = agent;
        }

        public void Enter()
        {
            _agent.ResetPath();
            _controller.ActivateHitbox(false);
        }

        public void Update()
        {
            if (_controller.Target == null)
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.PatrolState);
                return;
            }

            switch (_controller.PrimaryAttackType)
            {
                case AttackType.Melee:
                    HandleMelee();
                    break;
                case AttackType.Range:
                    HandleRanged();
                    break;
            }
        }

        public void Exit()
        {
            _controller.ActivateHitbox(false);
        }

        private void HandleMelee()
        {
            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);

            if (distance <= _controller.AttackDistance)
            {
                _controller.ActivateHitbox(true);
            }
            else
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.ChaseState);
            }
        }

        private void HandleRanged()
        {
            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);

            if (distance > _controller.AttackDistance)
            {
                _controller.TransitionToState(_controller.ChaseState);
                return;
            }

            if (Time.time - _lastShotTime < _controller.DamageCooldown) return;

            _lastShotTime = Time.time;

            GameObject projectile = GameObject.Instantiate(
                _controller.ProjectilePrefab,
                _controller.transform.position,
                Quaternion.identity
            );

            Vector2 direction = (_controller.Target.position - _controller.transform.position).normalized;
            var script = projectile.GetComponent<EnemyProjectile>();
            script.Damage = _controller.DamageAmount;
            script.Speed = _controller.ProjectileSpeed;
            script.SetDirection(direction);
        }
    }
}
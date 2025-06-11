using UnityEngine;
using UnityEngine.AI;

namespace Enemies.State
{
    public class ChaseState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private readonly NavMeshAgent _agent;

        public ChaseState(EnemyController enemy)
        {
            _enemy = enemy;
            _agent = _enemy.GetComponent<NavMeshAgent>();
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} started chasing.");
        }

        public void Update()
        {
            if (_enemy.Target == null)
            {
                _enemy.SetInitialState();
                return;
            }

            float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);

            if (distance <= _enemy.AttackDistance)
            {
                _enemy.SetAtackState();
                return;
            }

            _agent.SetDestination(_enemy.Target.position);
        }

        public void Exit()
        {
            Debug.Log($"{_enemy.name} stopped chasing.");
            _agent.ResetPath();
        }
    }
}
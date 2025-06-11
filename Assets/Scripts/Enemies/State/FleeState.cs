using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class FleeState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private readonly NavMeshAgent _agent;

        public FleeState(EnemyController enemy)
        {
            _enemy = enemy;
            _agent = _enemy.GetComponent<NavMeshAgent>();
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} entered Flee State.");
        }

        public void Update()
        {
            if (_enemy.Target == null)
            {
                _enemy.SetInitialState();
                return;
            }

            // Calcular dirección opuesta y moverse
            Vector2 fleeDirection = (_enemy.transform.position - _enemy.Target.position).normalized;
            Vector3 fleePosition = _enemy.transform.position + (Vector3)fleeDirection * 5f;

            _agent.SetDestination(fleePosition);
        }

        public void Exit()
        {
            Debug.Log($"{_enemy.name} exited Flee State.");
            _agent.ResetPath();
        }
    }
}
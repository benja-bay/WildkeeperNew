using UnityEngine;
using UnityEngine.AI;

namespace Enemies.State
{
    public class PatrolState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private readonly NavMeshAgent _agent;
        private int _currentPointIndex;
        private float _waitTimer;
        private bool _waiting;

        public PatrolState(EnemyController enemy)
        {
            _enemy = enemy;
            _agent = _enemy.GetComponent<NavMeshAgent>();
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} entered Patrol State.");
            _currentPointIndex = 0;
            _waitTimer = 0f;
            _waiting = false;

            if (_enemy.PatrolPoints.Length > 0)
                _agent.SetDestination(_enemy.PatrolPoints[_currentPointIndex].position);
        }

        public void Update()
        {
            if (_enemy.Target != null)
            {
                float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);

                // El ataque se maneja desde EnemyAttackHandler
                if (distance <= _enemy.VisionDistance)
                {
                    _enemy.SetVisionState();
                    return;
                }
            }

            if (_enemy.PatrolPoints.Length == 0) return;

            if (!_agent.pathPending && _agent.remainingDistance <= 0.2f)
            {
                if (!_waiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }

                _waitTimer += Time.deltaTime;

                if (_waitTimer >= _enemy.PatrolWaitTime)
                {
                    _currentPointIndex = (_currentPointIndex + 1) % _enemy.PatrolPoints.Length;
                    _agent.SetDestination(_enemy.PatrolPoints[_currentPointIndex].position);
                    _waiting = false;
                }
            }
        }

        public void Exit()
        {
            Debug.Log($"{_enemy.name} exited Patrol State.");
            _waiting = false;
            _waitTimer = 0f;
        }
    }
}
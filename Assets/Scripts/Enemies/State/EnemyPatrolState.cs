using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyPatrolState : IEnemyState
    {
        private EnemyController _controller;
        private NavMeshAgent _agent;
        private int _currentIndex = 0;
        private float _waitTimer;
        private bool _isWaiting;

        public EnemyPatrolState(EnemyController controller, NavMeshAgent agent)
        {
            _controller = controller;
            _agent = agent;
        }

        public void Enter()
        {
            if (_controller.PatrolPoints.Length > 0)
                _agent.SetDestination(_controller.PatrolPoints[_currentIndex].position);
        }

        public void Update()
        {
            if (_controller.Target != null)
            {
                _controller.OnVision(); // Correcto ahora que es público
                return;
            }

            if (_controller.PatrolPoints.Length == 0) return;

            if (_agent.remainingDistance <= 0.8f)
            {
                if (!_isWaiting)
                {
                    _isWaiting = true;
                    _waitTimer = _controller.PatrolWaitTime;
                }

                _waitTimer -= Time.deltaTime;

                if (_waitTimer <= 0f)
                {
                    _currentIndex = (_currentIndex + 1) % _controller.PatrolPoints.Length;
                    _agent.SetDestination(_controller.PatrolPoints[_currentIndex].position);
                    _isWaiting = false;
                }
            }
            
            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);
            if (distance <= _controller.AttackDistance)
            {
                _controller.TransitionToState(_controller.AttackState);
            }
            
            
        }

        public void Exit() { }
    }
}
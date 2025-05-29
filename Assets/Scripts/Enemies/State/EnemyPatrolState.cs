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
            // Si detecta al jugador, cambia a persecución
            if (_controller.Target != null)
            {
                _controller.OnVision(); // esto hará la transición
                return;
            }

            if (_controller.PatrolPoints == null || _controller.PatrolPoints.Length == 0)
                return;

            // Espera al llegar a un punto antes de moverse al siguiente
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
        }

        public void Exit() { }
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    // Estado de persecución del enemigo
    public class EnemyChaseState : IEnemyState
    {
        private EnemyController _controller;
        private NavMeshAgent _agent;

        public EnemyChaseState(EnemyController controller, NavMeshAgent agent)
        {
            _controller = controller;
            _agent = agent;
        }

        public void Enter() { }

        public void Update()
        {
            if (_controller.Target == null)
            {
                _controller.TransitionToState(_controller.PatrolState);
                return;
            }

            // Persigue al jugador
            _agent.SetDestination(_controller.Target.position);

            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);
            if (distance <= 1.5f)
            {
                _controller.TransitionToState(_controller.AttackState);
            }
        }

        public void Exit() { }
    }
}
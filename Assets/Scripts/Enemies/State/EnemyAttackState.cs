using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    // Estado de ataque del enemigo
    public class EnemyAttackState : IEnemyState
    {
        private EnemyController _controller;
        private NavMeshAgent _agent;

        public EnemyAttackState(EnemyController controller, NavMeshAgent agent)
        {
            _controller = controller;
            _agent = agent;
        }

        public void Enter()
        {
            // Detiene el movimiento y asegura que el hitbox esté desactivado inicialmente
            _agent.ResetPath();
            _controller.ActivateHitbox(false);
        }

        public void Update()
        {
            // Si el objetivo ya no está presente, vuelve a patrullar
            if (_controller.Target == null)
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.PatrolState);
                return;
            }

            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);

            // Si el jugador se aleja, vuelve al estado de persecución
            if (distance > _controller.AttackDistance + 0.2f)
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.ChaseState);
                return;
            }

            // Activa el hitbox solo si el jugador está en rango
            bool inAttackRange = distance <= _controller.AttackDistance;
            _controller.ActivateHitbox(inAttackRange);
        }

        public void Exit()
        {
            _controller.ActivateHitbox(false);
        }
    }
}
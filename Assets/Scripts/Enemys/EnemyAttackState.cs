using UnityEngine;
using UnityEngine.AI;

namespace Enemys
{
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
            _agent.ResetPath();

            // Activar el hitbox al entrar al estado de ataque
            _controller.ActivateHitbox(true);
        }

        public void Update()
        {
            if (_controller.Target == null)
            {
                _controller.ActivateHitbox(false); // Desactivar si pierde al jugador
                _controller.TransitionToState(_controller.PatrolState);
                return;
            }

            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);

            if (distance > _controller.AttackDistance + 0.2f) // pequeña tolerancia
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.ChaseState);
                return;
            }

            // Aquí podrías agregar animaciones o efectos de ataque si tienes alguno.
        }

        public void Exit()
        {
            _controller.ActivateHitbox(false); // Siempre desactivar el hitbox al salir del estado
        }
    }
}
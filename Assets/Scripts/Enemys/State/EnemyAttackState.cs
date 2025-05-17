using UnityEngine;
using UnityEngine.AI;

namespace Enemy
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
            _controller.ActivateHitbox(false); // Asegura que el hitbox está inactivo al entrar
        }

        public void Update()
        {
            if (_controller.Target == null)
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.PatrolState);
                return;
            }

            float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);

            // Cambia a persecución si el jugador se aleja
            if (distance > _controller.AttackDistance + 0.2f)
            {
                _controller.ActivateHitbox(false);
                _controller.TransitionToState(_controller.ChaseState);
                return;
            }

            // Activa el hitbox solo si el jugador está en rango de ataque
            bool inAttackRange = distance <= _controller.AttackDistance;
            _controller.ActivateHitbox(inAttackRange);
        }

        public void Exit()
        {
            _controller.ActivateHitbox(false);
        }
    }
}
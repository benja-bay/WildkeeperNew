using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class DeadState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private readonly NavMeshAgent _agent;
        private bool _destroyScheduled;

        public DeadState(EnemyController enemy)
        {
            _enemy = enemy;
            _agent = _enemy.GetComponent<NavMeshAgent>();
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} has died.");

            if (_agent != null)
            {
                _agent.isStopped = true;
                _agent.ResetPath();
            }

            _enemy.ActivateHitbox(false);
            _enemy.enabled = false; // Detiene Update si es necesario

            Animator anim = _enemy.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Die"); // Asegúrate de tener ese trigger en Animator
            }

            if (!_destroyScheduled)
            {
                Object.Destroy(_enemy.gameObject, 3f); // Destruye tras 3 segundos
                _destroyScheduled = true;
            }
        }

        public void Update()
        {
            // Nada que hacer aquí una vez muerto
        }

        public void Exit()
        {
            // Nunca debería salir de DeadState, pero se deja vacío por contrato
        }
    }
}

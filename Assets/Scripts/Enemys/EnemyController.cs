using UnityEngine;
using UnityEngine.AI;

namespace Enemys
{
    public class EnemyController : MonoBehaviour
    {
        private IEnemyState _currentState;
        private NavMeshAgent _agent;

        [Header("State References")]
        public EnemyPatrolState PatrolState;
        public EnemyChaseState ChaseState;
        public EnemyAttackState AttackState;

        [Header("General Settings")]
        public Transform[] PatrolPoints;
        public float PatrolWaitTime = 2f;
        public Transform Target;

        [Header("Attack Settings")]
        public int DamageAmount;
        public float DamageCooldown;
        public float AttackDistance;

        [Header("Components")]
        public EnemyMeleeHitbox MeleeHitbox;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            // Crear los estados con referencia a este controller y al agente
            PatrolState = new EnemyPatrolState(this, _agent);
            ChaseState = new EnemyChaseState(this, _agent);
            AttackState = new EnemyAttackState(this, _agent);
        }

        private void Start()
        {
            TransitionToState(PatrolState);

            // Configurar el hitbox si está asignado
            if (MeleeHitbox != null)
            {
                MeleeHitbox.Configure(DamageAmount, DamageCooldown, AttackDistance);
                MeleeHitbox.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public void TransitionToState(IEnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void SetTarget(Transform target)
        {
            Target = target;

            if (Target == null)
                TransitionToState(PatrolState);
            else
                TransitionToState(ChaseState);
        }

        // Activar o desactivar hitbox desde los estados
        public void ActivateHitbox(bool active)
        {
            if (MeleeHitbox != null)
                MeleeHitbox.gameObject.SetActive(active);
        }
    }
}

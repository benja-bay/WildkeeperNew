using UnityEngine;
using UnityEngine.AI;

namespace Enemy
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
        public int DamageAmount = 10;
        public float DamageCooldown = 1f;
        public float AttackDistance = 1f;

        [Header("Components")]
        public EnemyMeleeHitbox MeleeHitbox;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            PatrolState = new EnemyPatrolState(this, _agent);
            ChaseState = new EnemyChaseState(this, _agent);
            AttackState = new EnemyAttackState(this, _agent);
        }

        private void Start()
        {
            TransitionToState(PatrolState);

            if (MeleeHitbox != null)
            {
                MeleeHitbox.DamageAmount = DamageAmount;
                MeleeHitbox.DamageCooldown = DamageCooldown;
                MeleeHitbox.AttackDistance = AttackDistance;
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

        public void ActivateHitbox(bool active)
        {
            if (MeleeHitbox != null)
                MeleeHitbox.gameObject.SetActive(active);
        }
    }
}

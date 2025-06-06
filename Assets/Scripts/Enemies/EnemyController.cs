using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private IEnemyState _currentState;
        private NavMeshAgent _agent;
        private EnemyConfig _config;

        [Header("State References")]
        public EnemyPatrolState PatrolState;
        public EnemyChaseState ChaseState;
        public EnemyAttackState AttackState;

        [Header("General Settings")]
        public Transform[] PatrolPoints;
        public float PatrolWaitTime { get; private set; }
        public Transform Target;

        [Header("Enemy Stats")]
        [HideInInspector] public int DamageAmount;
        [HideInInspector] public float DamageCooldown;
        [HideInInspector] public float AttackDistance;
        public AttackType PrimaryAttackType { get; private set; }

        [Header("Ranged Settings")]
        public GameObject ProjectilePrefab;
        public float ProjectileSpeed = 10f;

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

        public void Initialize(EnemyConfig config)
        {
            _config = config;

            DamageAmount = config.Damage;
            DamageCooldown = config.AttackCooldown;
            AttackDistance = config.AttackDistance;
            PatrolWaitTime = config.PatrolWaitTime;
            ProjectilePrefab = config.ProjectilePrefab;
            ProjectileSpeed = config.ProjectileSpeed;

            if (_agent != null)
                _agent.speed = config.Speed;

            PrimaryAttackType = (config.AttackTypes != null && config.AttackTypes.Length > 0)
                ? config.AttackTypes[0]
                : AttackType.Melee;

            if (config.AttackTypes.Length > 1)
            {
                Debug.LogWarning($"Enemy {config.Name} has multiple attack types. Only the first will be used.");
            }

            SetInitialState();
        }

        private void SetInitialState()
        {
            if (_config.StartState.Length == 0)
            {
                Debug.LogWarning("No start state defined in EnemyConfig.");
                return;
            }

            switch (_config.StartState[0])
            {
                case BehaviorType.Patrol:
                    TransitionToState(PatrolState);
                    break;
                case BehaviorType.Chase:
                    TransitionToState(ChaseState);
                    break;
                case BehaviorType.Attack:
                    TransitionToState(AttackState);
                    break;
                default:
                    Debug.LogWarning("Unsupported start state: " + _config.StartState[0]);
                    break;
            }
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            if (Target == null) SetInitialState();
            else OnVision();
        }

        public void OnVision()
        {
            if (_config.OnVisionState.Length == 0) return;

            switch (_config.OnVisionState[0])
            {
                case BehaviorType.Chase:
                    TransitionToState(ChaseState);
                    break;
                case BehaviorType.Attack:
                    TransitionToState(AttackState);
                    break;
                default:
                    Debug.LogWarning("Unsupported vision state: " + _config.OnVisionState[0]);
                    break;
            }
        }

        public void TransitionToState(IEnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void ActivateHitbox(bool active)
        {
            if (MeleeHitbox != null)
                MeleeHitbox.gameObject.SetActive(active);
        }
    }
}
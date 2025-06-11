using Enemies.Factories;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private IEnemyState _currentState;
        private NavMeshAgent _agent;
        private EnemyConfig _config;

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
        public float ProjectileSpeed;

        [Header("Components")]
        public EnemyMeleeHitbox MeleeHitbox;

        private IEnemyState StartState;
        private IEnemyState VisionState;
        private IEnemyState AtackState;
        private EnemyStateFactory _stateFactory;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
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
            SetInitialState();
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public void Initialize(EnemyConfig config)
        {
            _config = config;

            _stateFactory = new EnemyStateFactory(this);
            StartState = _stateFactory.GetState(config.StartState.Length > 0 ? config.StartState[0] : BehaviorType.Idle);
            VisionState = _stateFactory.GetState(config.OnVisionState.Length > 0 ? config.OnVisionState[0] : BehaviorType.Chase);
            AtackState = _stateFactory.GetState(BehaviorType.Attack);

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
        }

        public void SetInitialState() => TransitionToState(StartState);
        public void SetVisionState() => TransitionToState(VisionState);
        public void SetAtackState() => TransitionToState(AtackState);

        public void SetTarget(Transform target) => Target = target;

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
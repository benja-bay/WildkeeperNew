using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    // Clase principal que controla el comportamiento general del enemigo usando una máquina de estados.
    public class EnemyController : MonoBehaviour
    {
        private IEnemyState _currentState;  // Estado actual del enemigo
        private NavMeshAgent _agent;        // Referencia al NavMeshAgent para movimiento

        [Header("State References")]
        public EnemyPatrolState PatrolState;
        public EnemyChaseState ChaseState;
        public EnemyAttackState AttackState;

        [Header("General Settings")]
        public Transform[] PatrolPoints;     // Puntos de patrulla
        public float PatrolWaitTime = 2f;    // Tiempo de espera en cada punto de patrulla
        public Transform Target;             // Objetivo actual del enemigo (el jugador)

        [Header("Enemy Stats")]
        [HideInInspector] public int DamageAmount;
        [HideInInspector] public float DamageCooldown;
        [HideInInspector] public float AttackDistance;
        public AttackType[] AttackTypes { get; private set; }



        [Header("Components")]
        public EnemyMeleeHitbox MeleeHitbox;

        private void Awake()
        {
            // Inicializa el NavMeshAgent y los estados
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            PatrolState = new EnemyPatrolState(this, _agent);
            ChaseState = new EnemyChaseState(this, _agent);
            AttackState = new EnemyAttackState(this, _agent);
        }

        private void Start()
        {
            // Inicia en el estado de patrulla
            TransitionToState(PatrolState);

            // Configura el hitbox de ataque
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
            // Llama al método Update del estado actual
            _currentState?.Update();
        }

        public void TransitionToState(IEnemyState newState)
        {
            // Transición entre estados
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void SetTarget(Transform target)
        {
            // Asigna un nuevo objetivo (jugador) y cambia de estado según corresponda
            Target = target;
            if (Target == null)
                TransitionToState(PatrolState);
            else
                TransitionToState(ChaseState);
        }

        public void ActivateHitbox(bool active)
        {
            // Activa o desactiva el hitbox de ataque
            if (MeleeHitbox != null)
                MeleeHitbox.gameObject.SetActive(active);
        }

        public void Initialize(EnemyConfig config)
        {
            DamageAmount = config.Damage;
            DamageCooldown = config.AttackCooldown;
            AttackDistance = config.AttackDistance;
            AttackTypes = config.AttackTypes;
        }
    }
}

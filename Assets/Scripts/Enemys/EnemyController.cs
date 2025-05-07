using UnityEngine;
using UnityEngine.AI;

namespace Enemys
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Patrol Settings")]
        [SerializeField] private Transform[] _patrolPoints;      // Puntos por los que el enemigo patrulla (definidos en la escena)
        [SerializeField] private float _patrolWaitTime = 2f;     // Tiempo que espera en cada punto antes de avanzar al siguiente

        private Transform _target;                               // Objetivo actual (jugador si es detectado)
        private NavMeshAgent _agent;                             // Componente para movimiento con navegación

        private int _currentPatrolIndex = 0;                     // Índice del punto de patrulla actual
        private float _waitTimer;                                // Temporizador de espera en puntos
        private bool _isWaiting;                                 // Si está esperando en un punto antes de moverse

        private void Start()
        {
            // Inicializa el NavMeshAgent y empieza la patrulla si hay puntos definidos
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            if (_patrolPoints.Length > 0)
            {
                _agent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
            }
        }

        private void Update()
        {
            // Si tiene un objetivo (jugador), lo persigue
            if (_target != null)
            {
                _agent.SetDestination(_target.position);
                return;
            }

            // Si no hay objetivo, ejecuta la lógica de patrullaje
            Patrol();
        }

        private void Patrol()
        {
            // Si no hay puntos de patrulla, no hace nada
            if (_patrolPoints.Length == 0) return;

            // Verifica si llegó al destino
            if (_agent.remainingDistance <= 0.1f)
            {
                // Comienza la espera en el punto actual
                if (!_isWaiting)
                {
                    _isWaiting = true;
                    _waitTimer = _patrolWaitTime;
                }

                // Cuenta el tiempo de espera
                _waitTimer -= Time.deltaTime;

                // Cuando termina de esperar, avanza al siguiente punto
                if (_waitTimer <= 0f)
                {
                    _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
                    _agent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
                    _isWaiting = false;
                }
            }
        }

        public void SetTarget(Transform target)
        {
            // Asigna o limpia el objetivo (jugador)
            _target = target;

            // Si se limpió el objetivo, retoma el patrullaje desde donde estaba
            if (_target == null && _patrolPoints.Length > 0)
            {
                _agent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
            }
        }
    }
}

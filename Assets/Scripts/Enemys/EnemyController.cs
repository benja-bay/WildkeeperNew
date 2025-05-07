using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemys
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            _agent.SetDestination(_target.position);
        }
    }
}

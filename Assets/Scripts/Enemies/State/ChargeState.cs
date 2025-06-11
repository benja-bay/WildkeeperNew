using UnityEngine;
using UnityEngine.AI;

namespace Enemies.State
{
    public class ChargeState : IEnemyState
    {
        private readonly EnemyController _enemy;
        private readonly NavMeshAgent _agent;

        private float _chargeTimer;
        private float _cooldownTimer;
        private bool _isCharging;
        private Vector3 _chargeDirection;

        public ChargeState(EnemyController enemy)
        {
            _enemy = enemy;
            _agent = _enemy.GetComponent<NavMeshAgent>();
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} entered Charge State.");
            _cooldownTimer = 0f;
            StartCharge();
        }

        public void Update()
        {
            if (_enemy.Target == null)
            {
                _enemy.SetInitialState();
                return;
            }

            if (_isCharging)
            {
                _chargeTimer += Time.deltaTime;

                if (_chargeTimer >= _enemy.ChargeDuration)
                {
                    StopCharge();
                }
            }
            else
            {
                _cooldownTimer += Time.deltaTime;
                if (_cooldownTimer >= _enemy.ChargeCooldown)
                {
                    StartCharge();
                }
            }
        }

        public void Exit()
        {
            Debug.Log($"{_enemy.name} exited Charge State.");
            StopCharge();
        }

        private void StartCharge()
        {
            _isCharging = true;
            _chargeTimer = 0f;

            _chargeDirection = (_enemy.Target.position - _enemy.transform.position).normalized;
            _agent.speed = _enemy.ChargeSpeed;
            _agent.SetDestination(_enemy.transform.position + _chargeDirection * 10f);
        }

        private void StopCharge()
        {
            _isCharging = false;
            _cooldownTimer = 0f;
            _agent.ResetPath();
        }
    }
}
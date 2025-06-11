using UnityEngine;

namespace Enemies.State
{
    public class IdleState : IEnemyState
    {
        private readonly EnemyController _enemy;

        public IdleState(EnemyController enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log($"{_enemy.name} entered Idle State.");
        }

        public void Update()
        {
            if (_enemy.Target == null) return;

            float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);

            // Ya no necesitamos atacar desde el estado
            if (distance <= _enemy.VisionDistance)
                _enemy.SetVisionState();
        }

        public void Exit()
        {
            Debug.Log($"{_enemy.name} exited Idle State.");
        }
    }
}
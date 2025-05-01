using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{
    public class PlayerRegenState : PlayerState
    {
        private float _regenDuration = 2f;
        private float _timer;
        private int _regenAmount = 10;

        public PlayerRegenState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            _timer = _regenDuration;
            Player.PlayerAnimation.PlayIdle(); // o animación de meditación
            Player.Move(Vector2.zero);
            Debug.Log("Regenerating health...");
        }

        public override void LogicUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                if (Player.TryGetComponent(out PlayerHealth health))
                {
                    health.Heal(_regenAmount);
                }

                // Volver al estado adecuado
                if (Player.inputHandler.movementInput == Vector2.zero)
                    StateMachine.ChangeState(Player.IdleState);
                else
                    StateMachine.ChangeState(Player.WalkState);
            }
        }
    }
}

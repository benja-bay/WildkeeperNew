using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{
    public class PlayerTakeDamageState : PlayerState
    {
        private float _stunDuration = 0.3f;
        private float _timer;

        public PlayerTakeDamageState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            _timer = _stunDuration;
            Player.PlayerAnimation.PlayIdle(); // O animación de "doler"
            Player.Move(Vector2.zero); // Detener movimiento al recibir daño
            Debug.Log("Player took damage!");
        }

        public override void LogicUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                // Volver al estado Idle o Walk según el input
                if (Player.inputHandler.movementInput == Vector2.zero)
                    StateMachine.ChangeState(Player.IdleState);
                else
                    StateMachine.ChangeState(Player.WalkState);
            }
        }
    }
}

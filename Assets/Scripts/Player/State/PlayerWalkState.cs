using UnityEngine;

namespace Player.State
{
    public class PlayerWalkState : PlayerState
    {
        public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            Player.PlayerAnimation.PlayWalk(Player.inputHandler.movementInput); // play walking animation
            
        }

        public override void HandleInput()
        {
            // si hacen click cambia al estado atack
            if (Player.inputHandler.attackPressed)
            {
                StateMachine.ChangeState(Player.MeleAttackState);
            }
            // If it does not move, the state changes to idle
            else if (Player.inputHandler.movementInput == Vector2.zero)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            Vector2 moveInput = Player.inputHandler.movementInput;
            Player.Move(moveInput); // player movement
        }

        public override void LogicUpdate()
        {
            Vector2 moveInput = Player.inputHandler.movementInput;
            Player.PlayerAnimation.PlayWalk(moveInput); // animation update
        }
    }
}

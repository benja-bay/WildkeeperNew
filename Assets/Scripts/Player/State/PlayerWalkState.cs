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
            // If it does not move, the state changes to idle
            if (Player.inputHandler.movementInput == Vector2.zero)
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

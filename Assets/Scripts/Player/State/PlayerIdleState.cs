using UnityEngine;

namespace Player.State
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            Player.PlayerAnimation.PlayIdle(); // Play idle animation
            Player.Move(Vector2.zero); // Stops the player's movement when enter on idle
        }

        public override void HandleInput()
        {
            // si hacen click cambia al estado atack 
            if (Player.inputHandler.attackPressed)
            {
                StateMachine.ChangeState(Player.MeleAttackState);
            }
            // If the player moves, the state changes to walking.
            else if (Player.inputHandler.movementInput != Vector2.zero) 
            {
                StateMachine.ChangeState(Player.WalkState);
            }
        }
    }
}

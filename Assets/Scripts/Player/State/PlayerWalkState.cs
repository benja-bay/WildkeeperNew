// ==============================
// PlayerWalkState.cs
// Handles player behavior while walking (movement input detected)
// ==============================

using UnityEngine;

namespace Player.State
{
    public class PlayerWalkState : PlayerState
    {
        // === Constructor ===
        public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            // === Play walking animation on state entry ===
            Player.PlayerAnimation.PlayWalk(Player.inputHandler.movementInput);
        }

        public override void HandleInput()
        {
            // === Handle transitions based on input ===

            // If attack input is pressed, switch to melee attack state
            if (Player.inputHandler.attackPressed)
            {
                StateMachine.ChangeState(Player.MeleAttackState);
            }
            else if (Player.inputHandler.interactPressed)
            {
                StateMachine.ChangeState(Player.InteractState); // ← NUEVA TRANSICIÓN
            }
            // If there's no movement input, switch to idle state
            else if (Player.inputHandler.movementInput == Vector2.zero)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }

        public override void LogicUpdate()
        {
            // === Update animation direction based on movement input ===
            Vector2 moveInput = Player.inputHandler.movementInput;
            Player.PlayerAnimation.PlayWalk(moveInput);
        }

        public override void PhysicsUpdate()
        {
            // === Move player based on input direction ===
            Vector2 moveInput = Player.inputHandler.movementInput;
            Player.Move(moveInput);
        }
    }
}

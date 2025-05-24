// ==============================
// PlayerIdleState.cs
// Handles player behavior while idle (no movement)
// ==============================

using UnityEngine;

namespace Player.State
{
    public class PlayerIdleState : PlayerState
    {
        // === Constructor ===
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            // === Play idle animation and stop movement ===
            Player.PlayerAnimation.PlayIdle();
            Player.Move(Vector2.zero);
        }

        public override void HandleInput()
        {
            // === Handle transitions based on input ===

            // If attack input is pressed, switch to melee attack state
            if (Player.inputHandler.attackPressed)
            {
                StateMachine.ChangeState(Player.MeleAttackState);
            }
            // si el interact es presinao cambia a interact state
            else if (Player.inputHandler.interactPressed)
            {
                StateMachine.ChangeState(Player.InteractState); // ← NUEVA TRANSICIÓN
            }
            // If movement input is detected, switch to walk state
            else if (Player.inputHandler.movementInput != Vector2.zero) 
            {
                StateMachine.ChangeState(Player.WalkState);
            }
        }
    }
}

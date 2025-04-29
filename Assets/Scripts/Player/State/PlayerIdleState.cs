using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.playerAnimation.PlayIdle(); // Play idle animation
        player.Move(Vector2.zero); // detiene el movimiento del jugador cuando entra en idle
    }

    public override void HandleInput()
    {
        if (player.inputHandler.movementInput != Vector2.zero) // si el player se mueve cambia el estado a caminando
        {
            stateMachine.ChangeState(player.walkState);
        }
    }
}

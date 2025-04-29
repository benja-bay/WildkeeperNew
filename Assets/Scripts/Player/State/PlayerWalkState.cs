using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.playerAnimation.PlayWalk(player.inputHandler.movementInput); // play walking animation
    }

    public override void HandleInput()
    {
  
        if (player.inputHandler.movementInput == Vector2.zero) // si no se mueve cambia el estado a icle
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate() // movimiento del player
    {
        Vector2 moveInput = player.inputHandler.movementInput;
        player.Move(moveInput);
    }

    public override void LogicUpdate()
    {
        Vector2 moveInput = player.inputHandler.movementInput;
        player.playerAnimation.PlayWalk(moveInput);
    }
}

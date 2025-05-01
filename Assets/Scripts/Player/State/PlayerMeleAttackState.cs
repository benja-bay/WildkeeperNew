using System.Collections;
using System.Collections.Generic;
using Player;
using Player.State;
using UnityEngine;

public class PlayerMeleAttackState : PlayerState
{
    public PlayerMeleAttackState(Player.Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //Ejecutar animacion
        //mensaje en consola
        //activar el hitbox
    }

    public override void HandleInput()
    {
        base.HandleInput();
        //Comprueba si se esta moviento
        //si se mueve cambia a estado walk
        //si no se mueve cambia estado idle
    }

    public override void Exit()
    {
        base.Exit();
        // detener animcion de ataque 
        //desactivar el hitbox
    }
}

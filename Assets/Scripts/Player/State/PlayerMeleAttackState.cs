using System.Collections;
using System.Collections.Generic;
using Player;
using Player.State;
using UnityEngine;

public class PlayerMeleAttackState : PlayerState
{
    private GameObject meleeHitbox;
    private float attackDuration = 0.4f;
    private float attackTimer;

    public PlayerMeleAttackState(Player.Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox) : base(player, stateMachine)
    {
        this.meleeHitbox = meleeHitbox; 
    }

    public override void Enter()
    {
        base.Enter();
        Player.isAttacking = true;
        
        //Ejecutar animacion
        //mensaje en consola
        Debug.Log("MeleHitbox activated");
        //activar el hitbox
        meleeHitbox.SetActive(true);
        
        attackTimer = attackDuration;
    }
    

    public override void HandleInput()
    {
        base.HandleInput();
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            StateMachine.ChangeState(Player.IdleState); // O el estado que corresponda
        }
        //Comprueba si se esta moviento
        //si se mueve cambia a estado walk
        //si no se mueve cambia estado idle
    }

    public override void Exit()
    {
        base.Exit();
        Player.isAttacking = false;
        
        // detener animcion de ataque 
        //desactivar el hitbox
        meleeHitbox.SetActive(false);
    }
}

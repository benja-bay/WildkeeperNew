using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    
    public virtual void Enter() { } // maneja cuando entra al estado
    
    public virtual void Exit() { } // maneja cuando sale del estado
    
    // handle input y logic update manejan los update del state
    public virtual void HandleInput() { } // maneja inputs dentro del estado
    
    public virtual void LogicUpdate() { } // maneja la logica dentro del estado
    
    public virtual void PhysicsUpdate() { } // maneja las fisicas del estado
    
}
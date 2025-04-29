using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState; // setea el estado actual en el estado que queremos que empieze 
        CurrentState.Enter(); // logica al entrar al estado
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit(); // ejecuta logica al salir
        CurrentState = newState; // cambia al nuevo estado
        CurrentState.Enter(); // logica al entrar al nuevo estado
    }
}

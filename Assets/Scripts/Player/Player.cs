using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f; // Player movement speed

    [HideInInspector] public PlayerInputHandler inputHandler; // Handles input
    [HideInInspector] public Rigidbody2D rb; // Physics movement
    [HideInInspector] public PlayerAnimation playerAnimation; // Handles animations

    private Animator animator; // Unity Animator component

    public PlayerStateMachine stateMachine; // controla transiciones entre estados
    public PlayerIdleState idleState; 
    public PlayerWalkState walkState; 

    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerAnimation = new PlayerAnimation(animator);

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine);
        walkState = new PlayerWalkState(this, stateMachine);
        // el "this" se refiere al player
    }

    void Start()
    {
        stateMachine.Initialize(idleState); // iniciamos la state machine en idle state
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput(); // ejecuta la logica de input en update
        stateMachine.CurrentState.LogicUpdate(); // ejecuta la logica general en update
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate(); // ejecuta las fisicas en fixed update
    }

    public void Move(Vector2 direction) // metodo para mover al jugador
    {
        rb.velocity = direction * moveSpeed; 
    }
}

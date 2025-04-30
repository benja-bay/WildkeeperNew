using Player.State;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 3f; // Player movement speed

        [HideInInspector] public PlayerInputHandler inputHandler; // Handles input
        [HideInInspector] public Rigidbody2D rb2D; // Physics movement
        [HideInInspector] public PlayerAnimation PlayerAnimation; // Handles animations
        [HideInInspector] public PlayerIdleState IdleState; 
        [HideInInspector] public PlayerWalkState WalkState; 
        
        private Animator _animator; // Unity Animator component
        private PlayerStateMachine _stateMachine; // Controls transitions between states

        void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            rb2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            PlayerAnimation = new PlayerAnimation(_animator);
            
            _stateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, _stateMachine);
            WalkState = new PlayerWalkState(this, _stateMachine);
        }

        void Start()
        {
            _stateMachine.Initialize(IdleState); // Starts state machine in idle state
        }

        void Update()
        {
            _stateMachine.CurrentState.HandleInput(); // Executes input logic on update
            _stateMachine.CurrentState.LogicUpdate(); // Executes general logic on update
        }

        void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate(); // Run physics on fixed update
        }

        public void Move(Vector2 direction) // Method to move the player
        {
            rb2D.velocity = direction * moveSpeed; 
        }
    }
}

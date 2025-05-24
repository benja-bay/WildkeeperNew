// ==============================
// Player.cs
// Main Player class managing input, animation, movement, and states
// ==============================

using Player.State;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        // === Movement Configuration ===
        [Header("Movement")]
        public float moveSpeed = 3f; // Player movement speed

        // === Hitbox Configuration ===
        [Header("Hitbox")]
        public GameObject meleeHitbox; // Reference to melee hitbox

        // === Internal Component References ===
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isInteracting;
        [HideInInspector] public PlayerInputHandler inputHandler; // Input handler
        [HideInInspector] public Rigidbody2D rb2D; // Rigidbody for movement
        [HideInInspector] public PlayerAnimation PlayerAnimation; // Animation handler

        // === State Instances ===
        [HideInInspector] public PlayerIdleState IdleState; 
        [HideInInspector] public PlayerWalkState WalkState; 
        [HideInInspector] public PlayerMeleAttackState MeleAttackState; 
        [HideInInspector] public PlayerInteractState InteractState; 

        // === Private Components ===
        private Animator _animator;
        private PlayerStateMachine _stateMachine; // Controls current player state

        void Awake()
        {
            // === Initialization of core components and player states ===
            inputHandler = GetComponent<PlayerInputHandler>();
            rb2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            PlayerAnimation = new PlayerAnimation(_animator);
            _stateMachine = new PlayerStateMachine();

            // Create instances of each state
            IdleState = new PlayerIdleState(this, _stateMachine);
            WalkState = new PlayerWalkState(this, _stateMachine);
            MeleAttackState = new PlayerMeleAttackState(this, _stateMachine, meleeHitbox);
            InteractState = new PlayerInteractState(this, _stateMachine, meleeHitbox);
        }

        void Start()
        {
            // === Set initial state to Idle ===
            _stateMachine.Initialize(IdleState);
        }

        void Update()
        {
            // === Delegate to state logic and input update ===
            _stateMachine.CurrentState.HandleInput();
            _stateMachine.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            // === Delegate to state physics update ===
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        public void Move(Vector2 direction)
        {
            // === Apply velocity to Rigidbody based on input direction and speed ===
            rb2D.velocity = direction * moveSpeed;
        }
    }
}

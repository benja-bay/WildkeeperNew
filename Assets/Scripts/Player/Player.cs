// ==============================
// Player.cs
// Main Player class managing input, animation, movement, and states
// ==============================

using Items;
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

        // === Attack Configuration ===
        [Header("Ranged")]
        [SerializeField] private GameObject _weaponObject; // Weapon GameObject for ranged attacks
        [SerializeField] private WeaponScript _weaponScript; // Script controlling weapon behavior
        [SerializeField] private WeaponAim _weaponAim; // Script handling weapon aiming
        [SerializeField] private ItemSO dartItem; // Item used as ammo for ranged attacks
        public ItemSO DartItem => dartItem; // Public getter for dartItem

        // === Internal Component References ===
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isShooting;
        [HideInInspector] public bool isInteracting;
        [HideInInspector] public PlayerInputHandler inputHandler; // Input handler
        [HideInInspector] public Rigidbody2D rb2D; // Rigidbody for movement
        [HideInInspector] public PlayerAnimation PlayerAnimation; // Animation handler
        public Inventory Inventory { get; private set; } // Player inventory

        [Header("Unlock Items")]
        [SerializeField] private ItemSO meleeUnlockItem; // Item used to unlock melee attack
        [SerializeField] private ItemSO rangedUnlockItem; // Item used to unlock ranged attack

        // === State Instances ===
        [HideInInspector] public PlayerIdleState IdleState; 
        [HideInInspector] public PlayerWalkState WalkState; 
        [HideInInspector] public PlayerMeleAttackState MeleAttackState; 
        [HideInInspector] public PlayerInteractState InteractState;
        [HideInInspector] public PlayerRangedAttackState RangedAttackState;
        [HideInInspector] public PlayerUseItemState UseItemState;

        // === Private Components ===
        private Animator _animator;
        private PlayerStateMachine _stateMachine; // Controls current player state
        private AttackMode _lastAttackMode;

        void Awake()
        {
            // === Initialization of core components and player states ===
            inputHandler = GetComponent<PlayerInputHandler>();
            rb2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            PlayerAnimation = new PlayerAnimation(_animator); // Setup animation handler
            _stateMachine = new PlayerStateMachine(); // Initialize state machine
            Inventory = new Inventory(); // Initialize empty inventory

            // Create instances of each state and inject required dependencies
            IdleState = new PlayerIdleState(this, _stateMachine);
            WalkState = new PlayerWalkState(this, _stateMachine);
            MeleAttackState = new PlayerMeleAttackState(this, _stateMachine, meleeHitbox);
            InteractState = new PlayerInteractState(this, _stateMachine, meleeHitbox);
            RangedAttackState = new PlayerRangedAttackState(this, _stateMachine, _weaponScript, _weaponAim);
            UseItemState = new PlayerUseItemState(this, _stateMachine);
        }

        void Start()
        {
            // === Set initial state to Idle ===
            _stateMachine.Initialize(IdleState);
        }

        void Update()
        {
            // === Handle attack mode switch via mouse scroll ===
            if (_lastAttackMode != inputHandler.CurrentAttackMode)
            {
                _lastAttackMode = inputHandler.CurrentAttackMode;

                // Show weapon only if ranged is selected, unlocked, and player has ammo
                if (_lastAttackMode == AttackMode.Ranged 
                    && RangedAttackState.IsUnlocked 
                    && Inventory.HasAmmo(DartItem))
                {
                    _weaponObject.SetActive(true);
                }
                else
                {
                    _weaponObject.SetActive(false);
                }
            }

            // Hide weapon if in ranged mode without unlock or ammo
            if (inputHandler.CurrentAttackMode == AttackMode.Ranged 
                && (!RangedAttackState.IsUnlocked || !Inventory.HasAmmo(DartItem)))
            {
                _weaponObject.SetActive(false);
            }

            // === Handle attack input ===
            if (inputHandler.attackPressed)
            {
                if (inputHandler.CurrentAttackMode == AttackMode.Melee && MeleAttackState.IsUnlocked)
                {
                    _weaponObject.SetActive(false); // Hide ranged weapon
                    _stateMachine.ChangeState(MeleAttackState);
                }
                else if (inputHandler.CurrentAttackMode == AttackMode.Ranged && RangedAttackState.IsUnlocked)
                {
                    _weaponObject.SetActive(true);
                    _stateMachine.ChangeState(RangedAttackState);
                }
                else
                {
                    Debug.Log("Attack mode not unlocked.");
                }
            }

            // === Handle item usage input ===
            if (inputHandler.useItemPressed)
            {
                // Use the first usable healing item found in inventory
                var berry = Inventory.GetFirstUsableItemOfType(ItemSO.ItemEffectType.Heal);
                if (berry != null)
                {
                    UseItemState.SetItemToUse(berry);
                    _stateMachine.ChangeState(UseItemState);
                    return; // Prevent attacking and item use in same frame
                }
                else
                {
                    Debug.Log("No items available.");
                }
            }

            // === Auto-unlock melee attack if item is in inventory ===
            if (!MeleAttackState.IsUnlocked && Inventory.GetItemCount(meleeUnlockItem) > 0)
            {
                MeleAttackState.Unlock();
                Debug.Log("Melee attack automatically unlocked via inventory.");
            }

            // === Auto-unlock ranged attack if item is in inventory ===
            if (!RangedAttackState.IsUnlocked && Inventory.GetItemCount(rangedUnlockItem) > 0)
            {
                RangedAttackState.Unlock();
                Debug.Log("Ranged attack automatically unlocked via inventory.");
            }

            // === Update current state logic and handle input ===
            _stateMachine.CurrentState.HandleInput();   // Process input for current state
            _stateMachine.CurrentState.LogicUpdate();   // Execute update logic for current state
        }

        void FixedUpdate()
        {
            // === Update current state's physics behavior ===
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        public void Move(Vector2 direction)
        {
            // === Apply velocity to Rigidbody based on input direction and speed ===
            rb2D.velocity = direction * moveSpeed;
        }
    }
}

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
        
        // === Attack Configuration ===
        [Header("Ranged")]
        [SerializeField] private GameObject _weaponObject;
        [SerializeField] private WeaponScript _weaponScript;
        [SerializeField] private WeaponAim _weaponAim;
        [SerializeField] private ItemSO dartItem;
        public ItemSO DartItem => dartItem;
        
        // === Internal Component References ===
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isShooting;
        [HideInInspector] public bool isInteracting;
        [HideInInspector] public PlayerInputHandler inputHandler; // Input handler
        [HideInInspector] public Rigidbody2D rb2D; // Rigidbody for movement
        [HideInInspector] public PlayerAnimation PlayerAnimation; // Animation handler
        public Inventory inventory { get; private set; }
        [Header("Unlock Items")]
        [SerializeField] private ItemSO meleeUnlockItem;
        [SerializeField] private ItemSO rangedUnlockItem;
        
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

            PlayerAnimation = new PlayerAnimation(_animator);
            _stateMachine = new PlayerStateMachine();
            inventory = new Inventory();

            // Create instances of each state
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
            // === Detectar si el jugador cambió el modo de ataque con la rueda del mouse ===
            if (_lastAttackMode != inputHandler.currentAttackMode)
            {
                _lastAttackMode = inputHandler.currentAttackMode;

                if (_lastAttackMode == AttackMode.Ranged 
                    && RangedAttackState.IsUnlocked 
                    && inventory.HasAmmo(DartItem))
                {
                    _weaponObject.SetActive(true);
                }
                else
                {
                    _weaponObject.SetActive(false);
                }
            }
            
            // Asegurar que se oculta el arma si estás en modo Ranged sin munición
            if (inputHandler.currentAttackMode == AttackMode.Ranged 
                && (!RangedAttackState.IsUnlocked || !inventory.HasAmmo(DartItem)))
            {
                _weaponObject.SetActive(false);
            }

            // === Si el jugador presiona el botón de ataque ===
            if (inputHandler.attackPressed)
            {
                if (inputHandler.currentAttackMode == AttackMode.Melee && MeleAttackState.IsUnlocked)
                {
                    _weaponObject.SetActive(false);
                    _stateMachine.ChangeState(MeleAttackState);
                }
                else if (inputHandler.currentAttackMode == AttackMode.Ranged && RangedAttackState.IsUnlocked)
                {
                    _weaponObject.SetActive(true);
                    _stateMachine.ChangeState(RangedAttackState);
                }
                else
                {
                    Debug.Log("Modo de ataque no desbloqueado.");
                }
            }
            if (inputHandler.useItemPressed)
            {
                // Acá seleccionamos un ítem automáticamente (por ahora simple)
                var berry = inventory.GetFirstUsableItemOfType(ItemSO.ItemEffectType.Heal);
                if (berry != null)
                {
                    UseItemState.SetItemToUse(berry);
                    _stateMachine.ChangeState(UseItemState);
                    return; // prevenimos que ataque y demás en el mismo frame
                }
                else
                {
                    Debug.Log("No tenés ítems.");
                }
            }
            
            // Desbloquear ataque melee automáticamente
            if (!MeleAttackState.IsUnlocked && inventory.GetItemCount(meleeUnlockItem) > 0)
            {
                MeleAttackState.Unlock();
                Debug.Log("Ataque melee desbloqueado automáticamente por inventario.");
            }

            // Desbloquear ataque a distancia automáticamente
            if (!RangedAttackState.IsUnlocked && inventory.GetItemCount(rangedUnlockItem) > 0)
            {
                RangedAttackState.Unlock();
                Debug.Log("Ataque a distancia desbloqueado automáticamente por inventario.");
            }

            // === Delegate to state logic and input update ===
            _stateMachine.CurrentState.HandleInput();   // Manejar entradas específicas del estado
            _stateMachine.CurrentState.LogicUpdate();   // Lógica de actualización del estado
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

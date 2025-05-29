// ==============================
// PlayerMeleAttackState.cs
// Handles player behavior during a melee attack
// ==============================

using UnityEngine;

namespace Player.State
{
    public class PlayerMeleAttackState : PlayerState
    {
        // === Attack Configuration ===
        private GameObject _meleeHitbox; // Reference to the melee hitbox GameObject
        private Hitbox _attackHitbox; // Script that manages the hitbox logic
        private float _attackDuration = 0.4f; // Duration of the attack animation
        private float _attackTimer;
        private bool _unlocked = false;

        // === Constructor ===
        public PlayerMeleAttackState(Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox) 
            : base(player, stateMachine)
        {
            _meleeHitbox = meleeHitbox;
            _attackHitbox = meleeHitbox.GetComponent<Hitbox>();
        }

        public override void Enter()
        {
            base.Enter();
            
            if (!_unlocked)
            {
                Debug.Log("Ataque melee no desbloqueado.");
                StateMachine.ChangeState(Player.IdleState);
                return;
            }
            
            Player.isAttacking = true;

            // === Initialize and activate melee hitbox ===
            _attackHitbox.Initialize(Player, Player.GetComponent<PlayerInputHandler>(), Player.transform);
            _attackHitbox.SetMode(HitboxMode.KAttack);
            _attackHitbox.UpdatePositionAndRotation();

            Debug.Log("MeleeHitbox activated");

            // Stop movement and play attack animation
            Player.Move(Vector2.zero);
            _meleeHitbox.SetActive(true);
            _attackTimer = _attackDuration;

            Vector2 mouseDirection = Player.inputHandler.MouseDirection;
            Player.PlayerAnimation.PlayMeleeAttack(mouseDirection);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            _attackTimer -= Time.deltaTime;

            // === Update hitbox direction while attacking ===
            _attackHitbox.UpdatePositionAndRotation();

            // === End of attack: transition to appropriate state ===
            if (_attackTimer <= 0f)
            {
                if (Player.inputHandler.movementInput != Vector2.zero)
                {
                    StateMachine.ChangeState(Player.WalkState);
                }
                else
                {
                    StateMachine.ChangeState(Player.IdleState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            // === Cleanup after attack ===
            Player.isAttacking = false;
            Player.PlayerAnimation.StopMeleeAttack();
            _meleeHitbox.SetActive(false);
        }
        
        public void Unlock()
        {
            _unlocked = true;
        }

        public bool IsUnlocked => _unlocked;
    }
}
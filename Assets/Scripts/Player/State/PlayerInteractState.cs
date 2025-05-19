using UnityEngine;

namespace Player.State
{
    public class PlayerInteractState : PlayerState
    {
        // === Attack Configuration ===
        private GameObject _meleeHitbox; // Reference to the melee hitbox GameObject
        private Hitbox _attackHitbox; // Script that manages the hitbox logic

        // === Constructor ===
        public PlayerInteractState(Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox) 
            : base(player, stateMachine)
        {
            _meleeHitbox = meleeHitbox;
            _attackHitbox = meleeHitbox.GetComponent<Hitbox>();
        }
        
        public override void Enter()
        {
            // === stop movement ===
            Player.Move(Vector2.zero);
        }
        
        public override void Exit()
        {
            base.Exit();
        }
        
    }
}

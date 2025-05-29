using UnityEngine;

namespace Player.State
{
    public class PlayerInteractState : PlayerState
    {
        // === Interaction Configuration ===
        private GameObject _meleeHitbox; // Reusing melee hitbox
        private Hitbox _interactionHitbox; // Reference to the hitbox logic
        private float _interactionDuration = 0.3f; // Duration of the interaction
        private float _interactionTimer;

        public PlayerInteractState(Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox)
            : base(player, stateMachine)
        {
            _meleeHitbox = meleeHitbox;
            _interactionHitbox = meleeHitbox.GetComponent<Hitbox>();
        }

        public override void Enter()
        {
            base.Enter();
            Player.isInteracting = true;

            // Initialize hitbox and switch to Interact mode
            _interactionHitbox.Initialize(Player, Player.GetComponent<PlayerInputHandler>(), Player.transform);
            _interactionHitbox.SetMode(HitboxMode.kInteract);
            _interactionHitbox.UpdatePositionAndRotation();

            Debug.Log("Interaction hitbox activated");

            // Stop movement and play interaction animation (optional)
            Player.Move(Vector2.zero);
            _meleeHitbox.SetActive(true);
            _interactionTimer = _interactionDuration;

            Vector2 mouseDirection = Player.inputHandler.MouseDirection;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            _interactionTimer -= Time.deltaTime;

            // Keep hitbox in correct position
            _interactionHitbox.UpdatePositionAndRotation();

            // Finish interaction
            if (_interactionTimer <= 0f)
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
            Player.isInteracting = false;
            _meleeHitbox.SetActive(false);
        }
    }
}

// ==============================
// PlayerUseItemState.cs
// Handles the logic and timing when the player uses a consumable item
// ==============================

using Items;
using UnityEngine;

namespace Player.State
{
    public class PlayerUseItemState : PlayerState
    {
        private float _useDuration = 0.5f;             // Time it takes to use an item
        private float _timer;                          // Internal timer for use duration
        private ItemSO _itemToUse;                     // The item to be consumed

        public PlayerUseItemState(Player player, PlayerStateMachine stateMachine)
            : base(player, stateMachine) { }

        // Set the item that should be used when entering this state
        public void SetItemToUse(ItemSO item)
        {
            _itemToUse = item;
        }

        // Called once when this state becomes active
        public override void Enter()
        {
            base.Enter();
            _timer = _useDuration;
            Player.Move(Vector2.zero); // Prevent movement during item use

            bool used = Player.Inventory.UseItem(_itemToUse, Player);
            if (!used)
            {
                Debug.LogWarning($"Failed to use item: {_itemToUse.itemName}");
                StateMachine.ChangeState(Player.IdleState);
            }
        }

        // Called every frame to update logic and transition when done
        public override void HandleInput()
        {
            base.HandleInput();

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                if (Player.inputHandler.movementInput != Vector2.zero)
                    StateMachine.ChangeState(Player.WalkState);
                else
                    StateMachine.ChangeState(Player.IdleState);
            }
        }

        // Called when exiting the item use state
        public override void Exit()
        {
            base.Exit();
        }
    }
}

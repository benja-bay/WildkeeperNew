using Items;
using UnityEngine;

namespace Player.State
{
    public class PlayerUseItemState : PlayerState
    {
        private float _useDuration = 0.5f;
        private float _timer;
        private ItemSo _itemToUse;

        public PlayerUseItemState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

        public void SetItemToUse(ItemSo item)
        {
            _itemToUse = item;
        }

        public override void Enter()
        {
            base.Enter();
            _timer = _useDuration;
            Player.Move(Vector2.zero);

            bool used = Player.Inventory.UseItem(_itemToUse, Player);
            if (!used)
            {
                Debug.LogWarning($"No se pudo usar el ítem {_itemToUse.itemName}");
                StateMachine.ChangeState(Player.IdleState);
            }
        }

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

        public override void Exit()
        {
            base.Exit();
        }
    }
}

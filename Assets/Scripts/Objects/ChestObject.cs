// ==============================
// ChestObject.cs
// Interactable chest that grants items to the player's inventory once
// ==============================

using Items;
using UnityEngine;

namespace Objects
{
    public class ChestObject : MonoBehaviour, IInteractable
    {
        [Header("Visuals")]
        [Tooltip("Sprite displayed when the chest has already been used")]
        [SerializeField] private Sprite usedSprite;

        private bool _hasBeenUsed = false; // Ensures the chest is only opened once
        private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer

        [System.Serializable]
        public struct ChestItem
        {
            public ItemSO item;     // Item contained in the chest
            public int quantity;    // Quantity of the item
        }

        [SerializeField] private ChestItem[] contents; // List of items inside the chest

        private void Awake()
        {
            // === Cache the SpriteRenderer attached to this object ===
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                Debug.LogError("ChestObject: No SpriteRenderer found on this object.");
            }
        }

        public void Interact(Player.Player player)
        {
            // === Prevent chest from being used multiple times ===
            if (_hasBeenUsed)
            {
                Debug.Log("There are no more items here.");
                return;
            }

            // === Access the player's inventory to store items ===
            var inventory = player.Inventory;
            if (inventory == null)
            {
                Debug.LogError("Player does not have an Inventory component.");
                return;
            }

            // === Add each item in the chest to the player's inventory ===
            foreach (var chestItem in contents)
            {
                inventory.AddItem(chestItem.item, chestItem.quantity);
                Debug.Log($"Added {chestItem.quantity}x {chestItem.item.itemName}");
            }

            _hasBeenUsed = true;

            // === Update chest appearance to indicate it has been used ===
            if (usedSprite != null && _spriteRenderer != null)
            {
                _spriteRenderer.sprite = usedSprite;
            }
        }
    }
}
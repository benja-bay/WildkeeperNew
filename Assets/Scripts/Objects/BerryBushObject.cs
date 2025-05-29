// ==============================
// BerryBushObject.cs
// Interactable bush that gives randomized items to the player once, with animation
// ==============================

using Items;
using UnityEngine;

namespace Objects
{
    public class BerryBushObject : MonoBehaviour, IInteractable
    {
        [Header("Animator")]
        [Tooltip("Animator controlling the bush's animations")]
        [SerializeField] private Animator animator;

        private bool _hasBeenUsed = false; // Ensures the bush can only be collected once

        [System.Serializable]
        public struct BushItem
        {
            public ItemSo item;       // Item to be collected
            public int minQuantity;   // Minimum amount given
            public int maxQuantity;   // Maximum amount given
        }

        [SerializeField] private BushItem[] contents; // List of collectible items in the bush

        private void Awake()
        {
            // === Cache Animator if not set in Inspector ===
            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                {
                    Debug.LogError("BerryBushObject: No Animator found on this object.");
                }
            }
        }

        public void Interact(Player.Player player)
        {
            // === Prevent bush from being used more than once ===
            if (_hasBeenUsed)
            {
                Debug.Log("This bush has already been harvested.");
                return;
            }

            // === Access the player's inventory ===
            var inventory = player.Inventory;
            if (inventory == null)
            {
                Debug.LogError("Player does not have an Inventory component.");
                return;
            }

            // === Randomly determine and give items to player ===
            foreach (var bushItem in contents)
            {
                int amount = Random.Range(bushItem.minQuantity, bushItem.maxQuantity + 1);
                inventory.AddItem(bushItem.item, amount);
                Debug.Log($"Collected {amount}x {bushItem.item.itemName}");
            }

            _hasBeenUsed = true;

            // === Play used animation if available ===
            if (animator != null)
            {
                animator.SetTrigger("Used");
            }
        }
    }
}
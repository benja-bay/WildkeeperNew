// ==============================
// Inventory.cs
// Manages player's items, including usage, quantity tracking, and effects
// ==============================

using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Player
{
    public class Inventory
    {
        // === Stored Items ===
        private Dictionary<ItemSo, int> _items = new(); // Stores each item and its quantity

        // Adds item to inventory, capping at its max allowed amount
        public void AddItem(ItemSo item, int quantity = 1)
        {
            if (_items.ContainsKey(item))
            {
                _items[item] = Mathf.Min(_items[item] + quantity, item.maxAmount);
            }
            else
            {
                _items[item] = Mathf.Min(quantity, item.maxAmount);
            }

            Debug.Log($"Now you have {_items[item]}x {item.itemName}");
        }

        // Uses an item and applies its effect to the player
        public bool UseItem(ItemSo item, Player player)
        {
            if (!_items.ContainsKey(item) || _items[item] <= 0)
                return false;

            _items[item]--;

            switch (item.effectType)
            {
                case ItemSo.ItemEffectType.Heal:
                    player.GetComponent<PlayerHealth>()?.Regenerate(item.value);
                    break;

                case ItemSo.ItemEffectType.Ammo:
                    // Ammo usage handled elsewhere
                    break;

                case ItemSo.ItemEffectType.UnlockMelee:
                    player.MeleAttackState.Unlock();
                    break;

                case ItemSo.ItemEffectType.UnlockRanged:
                    player.RangedAttackState.Unlock();
                    break;
            }

            return true;
        }

        // Returns the current quantity of a given item
        public int GetItemCount(ItemSo item)
        {
            return _items.TryGetValue(item, out int count) ? count : 0;
        }

        // Finds the first usable item of a specific effect type
        public ItemSo GetFirstUsableItemOfType(ItemSo.ItemEffectType effectType)
        {
            foreach (var kvp in _items)
            {
                if (kvp.Key.effectType == effectType && kvp.Value > 0)
                    return kvp.Key;
            }
            return null;
        }

        // Reduces ammo count by one if available
        public bool ConsumeAmmo(ItemSo ammoItem)
        {
            if (!_items.ContainsKey(ammoItem) || _items[ammoItem] <= 0)
                return false;

            _items[ammoItem]--;
            return true;
        }

        // Checks if the player has ammo available
        public bool HasAmmo(ItemSo ammoItem)
        {
            return _items.TryGetValue(ammoItem, out int count) && count > 0;
        }
    }
}

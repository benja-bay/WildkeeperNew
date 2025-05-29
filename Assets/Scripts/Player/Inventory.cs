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
        private Dictionary<ItemSO, int> _items = new(); // Stores items and their quantities

        public void AddItem(ItemSO item, int quantity = 1)
        {
            // === Add item to inventory, respecting max stack amount ===
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

        public bool UseItem(ItemSO item, Player player)
        {
            // === Consume item and apply its effect to the player ===
            if (!_items.ContainsKey(item) || _items[item] <= 0)
                return false;

            _items[item]--;

            // Apply item effect based on its type
            switch (item.effectType)
            {
                case ItemSO.ItemEffectType.Heal:
                    player.GetComponent<PlayerHealth>()?.Regenerate(item.value);
                    break;
                case ItemSO.ItemEffectType.Ammo:
                    // Reserved for future ammo system
                    break;
                case ItemSO.ItemEffectType.UnlockMelee:
                    player.MeleAttackState.Unlock();
                    break;
                case ItemSO.ItemEffectType.UnlockRanged:
                    player.RangedAttackState.Unlock();
                    break;
            }

            return true;
        }

        public int GetItemCount(ItemSO item)
        {
            // === Return current quantity of a given item ===
            return _items.TryGetValue(item, out int count) ? count : 0;
        }

        public ItemSO GetFirstUsableItemOfType(ItemSO.ItemEffectType effectType)
        {
            // === Find the first usable item matching a specific effect type ===
            foreach (var kvp in _items)
            {
                if (kvp.Key.effectType == effectType && kvp.Value > 0)
                    return kvp.Key;
            }
            return null;
        }

        public bool ConsumeAmmo(ItemSO ammoItem)
        {
            // === Reduce ammo count by one if available ===
            if (!_items.ContainsKey(ammoItem) || _items[ammoItem] <= 0)
                return false;

            _items[ammoItem]--;
            return true;
        }

        public bool HasAmmo(ItemSO ammoItem)
        {
            // === Check if player has ammo available ===
            return _items.TryGetValue(ammoItem, out int count) && count > 0;
        }
    }
}

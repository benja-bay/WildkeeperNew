using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Inventory
    {
        private Dictionary<ItemSO, int> _items = new();

        public void AddItem(ItemSO item, int quantity = 1)
        {
            if (item.isStackable)
            {
                if (_items.ContainsKey(item))
                    _items[item] += quantity;
                else
                    _items[item] = quantity;
            }
            else
            {
                if (!_items.ContainsKey(item))
                    _items[item] = 1; // Solo uno
                else
                    Debug.Log($"El item '{item.itemName}' ya está en el inventario (no stackeable).");
            }
        }

        public bool UseItem(ItemSO item, Player player)
        {
            if (!_items.ContainsKey(item) || _items[item] <= 0)
                return false;

            _items[item]--;

            switch (item.effectType)
            {
                case ItemSO.ItemEffectType.Heal:
                    player.GetComponent<PlayerHealth>()?.Regenerate(item.value);
                    break;
                case ItemSO.ItemEffectType.Ammo:
                    // Implementar si hay sistema de munición
                    break;
                case ItemSO.ItemEffectType.UnlockMelee:
                    // player.MeleAttackState.Unlock();
                    break;
                case ItemSO.ItemEffectType.UnlockRanged:
                    // player.RangedAttackState.Unlock();
                    break;
            }

            return true;
        }

        public int GetItemCount(ItemSO item)
        {
            return _items.TryGetValue(item, out int count) ? count : 0;
        }

        public ItemSO GetFirstUsableItemOfType(ItemSO.ItemEffectType effectType)
        {
            foreach (var kvp in _items)
            {
                if (kvp.Key.effectType == effectType && kvp.Value > 0)
                    return kvp.Key;
            }
            return null;
        }
    }
}

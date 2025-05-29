using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Inventory
    {
        private Dictionary<ItemSO, int> _items = new();

        public void AddItem(ItemSO item, int quantity = 1)
        {
            if (_items.ContainsKey(item))
            {
                _items[item] = Mathf.Min(_items[item] + quantity, item.maxAmount);
            }
            else
            {
                _items[item] = Mathf.Min(quantity, item.maxAmount);
            }

            Debug.Log($"Ahora tenés {_items[item]}x {item.itemName}");
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
        
        public bool ConsumeAmmo(ItemSO ammoItem)
        {
            if (!_items.ContainsKey(ammoItem) || _items[ammoItem] <= 0)
                return false;

            _items[ammoItem]--;
            return true;
        }

        public bool HasAmmo(ItemSO ammoItem)
        {
            return _items.TryGetValue(ammoItem, out int count) && count > 0;
        }
        
    }
}

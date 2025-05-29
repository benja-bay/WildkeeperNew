// ==============================
// ItemSO.cs
// Defines item data as ScriptableObject for use in inventory and game interactions
// ==============================
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class ItemSo : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public string description;
        public ItemEffectType effectType; // Determines how the item behaves
        public int value; // used for, healing amount
        public int maxAmount = 1; // Maximum quantity allowed in inventory

        public enum ItemEffectType
        {
            Heal,
            Ammo,
            UnlockMelee,
            UnlockRanged
        }
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public bool isStackable = true;
    public ItemEffectType effectType;
    public int value;

    public enum ItemEffectType
    {
        Heal,
        Ammo,
        UnlockMelee,
        UnlockRanged
    }
}

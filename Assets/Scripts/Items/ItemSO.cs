using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public ItemEffectType effectType;
    public int value;       // Por ejemplo, curación o munición
    public int maxAmount = 1; // Cantidad máxima que se puede tener

    public enum ItemEffectType
    {
        Heal,
        Ammo,
        UnlockMelee,
        UnlockRanged
    }
}

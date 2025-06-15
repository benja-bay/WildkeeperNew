using System.Collections.Generic;
using UnityEngine;
using Items;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [Header("Vida del jugador")]
    public int currentHealth;
    public int maxHealth;

    [Header("Inventario del jugador")]
    public Dictionary<ItemSO, int> inventory = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (!inventory.ContainsKey(item))
            inventory[item] = 0;
        inventory[item] += quantity;
    }

    public int GetItemCount(ItemSO item)
    {
        return inventory.ContainsKey(item) ? inventory[item] : 0;
    }

    public bool HasKey(string keyID)
    {
        foreach (var pair in inventory)
        {
            if (!string.IsNullOrEmpty(pair.Key.keyID) && pair.Key.keyID == keyID && pair.Value > 0)
                return true;
        }
        return false;
    }
}

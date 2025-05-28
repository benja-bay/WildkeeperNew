using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : MonoBehaviour, IInteractable
{
    [Header("Visuales")]
    [Tooltip("Sprite que se mostrará cuando el objeto ya haya sido usado")]
    [SerializeField] private Sprite usedSprite;

    private bool _hasBeenUsed = false; // control de uso unico
    private SpriteRenderer _spriteRenderer; // Referencia al SpriteRenderer actual
    
    [System.Serializable]
    public struct ChestItem
    {
        public ItemSO item;
        public int quantity;
    }

    [SerializeField] private ChestItem[] contents;
    
    private void Awake()
    {
        // Obtenemos el SpriteRenderer en el mismo objeto
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("ChestObject: No se encontró un SpriteRenderer en este objeto.");
        }
    }

    public void Interact(Player.Player player)
    {
        if (_hasBeenUsed) // comprobacion de uso unico
        {
            Debug.Log("No quedan objetos aqui.");
            return;
        }
        
        var inventory = player.inventory;
        if (inventory == null)
        {
            Debug.LogError("El jugador no tiene un componente Inventory.");
            return;
        }

        foreach (var chestItem in contents)
        {
            inventory.AddItem(chestItem.item, chestItem.quantity);
            Debug.Log($"Agregado {chestItem.quantity} de {chestItem.item.itemName}");
        }

        _hasBeenUsed = true;
        
        if (usedSprite != null && _spriteRenderer != null)
        {
            _spriteRenderer.sprite = usedSprite;
        }
        
    }
    
}

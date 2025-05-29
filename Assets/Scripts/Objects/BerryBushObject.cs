using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class BerryBushObject : MonoBehaviour, IInteractable
{
    [Header("Animator")]
    [Tooltip("Animator que controla las animaciones del arbusto")]
    [SerializeField] private Animator animator;

    private bool _hasBeenUsed = false;
    
    [System.Serializable]
    public struct BushItem
    {
        public ItemSO item;
        public int minQuantity;
        public int maxQuantity;
    }

    [SerializeField] private BushItem[] contents;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("BerryBushObject: No se encontró un Animator en este objeto.");
            }
        }
    }

    public void Interact(Player.Player player)
    {
        if (_hasBeenUsed)
        {
            Debug.Log("Este arbusto ya fue recolectado.");
            return;
        }

        var inventory = player.inventory;
        if (inventory == null)
        {
            Debug.LogError("El jugador no tiene un componente Inventory.");
            return;
        }

        foreach (var bushItem in contents)
        {
            int amount = Random.Range(bushItem.minQuantity, bushItem.maxQuantity + 1);
            inventory.AddItem(bushItem.item, amount);
            Debug.Log($"Recolectado {amount} de {bushItem.item.itemName}");
        }

        _hasBeenUsed = true;

        if (animator != null)
        {
            animator.SetTrigger("Used");
        }
    }
}
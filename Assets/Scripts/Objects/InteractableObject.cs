using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact(Player.Player player)
    {
        if (player == null)
        {
            Debug.LogWarning("Se intentó interactuar con un objeto sin un jugador válido.");
            return;
        }

        Debug.Log($"¡{player.name} ha interactuado con el objeto de prueba!");
    }
}

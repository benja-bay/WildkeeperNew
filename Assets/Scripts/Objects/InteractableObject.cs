using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact(Player.Player player)
    {
        Debug.Log($"¡{player.name} ha interactuado con el objeto de prueba!");
    }
}

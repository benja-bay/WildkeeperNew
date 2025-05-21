using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class HealingObject : MonoBehaviour, IInteractable
{
    [Tooltip("Porcentaje de la vida actual que se va a restaurar (entre 0 y 1)")]
    [Range(0f, 1f)]
    [SerializeField] private float healPercentage = 0.3f;

    private bool _hasBeenUsed = false; // control de uso unico

    public void Interact(Player.Player player)
    {
        if (_hasBeenUsed) // comprobacion de uso unico
        {
            Debug.Log("Este objeto de curación ya fue usado.");
            return;
        }

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health == null)
        {
            Debug.LogWarning("El jugador no tiene un componente PlayerHealth.");
            return;
        }

        // calcula cuanta vida cura en base a su vida maxima y el porcentaje establecido 
        int maxHealth = health.GetMaxHealth();
        int healAmount = Mathf.CeilToInt(maxHealth * healPercentage);

        if (healAmount <= 0)
        {
            Debug.Log("El valor de curación es cero o negativo.");
            return;
        }

        health.Regenerate(healAmount);

        Debug.Log($"El jugador fue curado por {healAmount} puntos de vida ({healPercentage * 100}% de su vida máxima).");

        _hasBeenUsed = true;
        
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
}

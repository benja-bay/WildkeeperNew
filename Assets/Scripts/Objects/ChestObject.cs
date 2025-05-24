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
        
        Debug.Log($"items obtenidos");

        _hasBeenUsed = true;
        
        if (usedSprite != null && _spriteRenderer != null)
        {
            _spriteRenderer.sprite = usedSprite;
        }
        
    }
    
}

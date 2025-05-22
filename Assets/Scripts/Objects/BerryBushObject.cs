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

        Debug.Log("Frutas recolectadas del arbusto");

        _hasBeenUsed = true;

        if (animator != null)
        {
            animator.SetTrigger("Used"); // Dispara la animación de caída de frutas
        }
    }
}
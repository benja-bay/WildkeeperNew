// ==============================
// HealingObject.cs
// Interactable healing object that restores a percentage of player's health once
// ==============================

using Player;
using UnityEngine;

namespace Objects
{
    public class HealingObject : MonoBehaviour, IInteractable
    {
        [Tooltip("Percentage of max health to restore (between 0 and 1)")]
        [Range(0f, 1f)]
        [SerializeField] private float healPercentage = 0.3f;

        [Header("Visuals")]
        [Tooltip("Sprite displayed when the object has already been used")]
        [SerializeField] private Sprite usedSprite;

        private bool _hasBeenUsed = false; // Ensures the object can only be used once
        private SpriteRenderer _spriteRenderer; // Reference to the current SpriteRenderer

        private void Awake()
        {
            // === Cache the SpriteRenderer component attached to this object ===
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                Debug.LogError("HealingObject: No SpriteRenderer found on this object.");
            }
        }

        public void Interact(Player.Player player)
        {
            // === Prevent multiple uses of the healing object ===
            if (_hasBeenUsed)
            {
                Debug.Log("This healing object has already been used.");
                return;
            }

            // === Attempt to get the PlayerHealth component from the player ===
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health == null)
            {
                Debug.LogWarning("Player does not have a PlayerHealth component.");
                return;
            }

            // === Calculate healing amount based on max health and configured percentage ===
            int maxHealth = health.GetMaxHealth();
            int healAmount = Mathf.CeilToInt(maxHealth * healPercentage);

            if (healAmount <= 0)
            {
                Debug.Log("Healing amount is zero or negative.");
                return;
            }

            // === Heal the player ===
            health.Regenerate(healAmount);
            Debug.Log($"Player healed for {healAmount} HP ({healPercentage * 100}% of max health).");

            _hasBeenUsed = true;

            // === Update visual to indicate object has been used ===
            if (usedSprite != null && _spriteRenderer != null)
            {
                _spriteRenderer.sprite = usedSprite;
            }
        }
    }
}

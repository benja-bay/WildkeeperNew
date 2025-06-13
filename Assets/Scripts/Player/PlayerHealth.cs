// ==============================
// PlayerHealth.cs
// Extends Health.cs to provide visual feedback for damage and healing, and updates HUD
// ==============================

using Systems;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        // === Visual Feedback Configuration ===
        [Header("Flash settings")]
        [SerializeField] private SpriteRenderer spriteRenderer; // Reference to player's sprite
        [SerializeField] private Color damageColor = Color.red; // Color when taking damage
        [SerializeField] private float flashDuration = 0.1f; // Duration of damage flash

        [SerializeField] private Color healColor = Color.green;   // Color when healing
        [SerializeField] private float healDuration = 0.3f;       // Duration of heal flash

        private Color _originalColor; // Original sprite color (restored after flash)

        // Called when player is healed through an item or effect
        public void Regenerate(int amount)
        {
            Heal(amount);
            StartCoroutine(FlashGreen());
        }

        // Called when player takes damage (includes visual feedback)
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            StartCoroutine(FlashRed());
        }

        // Coroutine to flash red briefly when damaged
        private System.Collections.IEnumerator FlashRed()
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = _originalColor;
        }

        // Coroutine to flash green briefly when healed
        private System.Collections.IEnumerator FlashGreen()
        {
            spriteRenderer.color = healColor;
            yield return new WaitForSeconds(healDuration);
            spriteRenderer.color = _originalColor;
        }

        // Called when player health reaches 0
        public override void Die()
        {
            base.Die();
            // TODO: Add additional logic like death animation or game restart
        }

        // Setup initial color reference
        private void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            _originalColor = spriteRenderer.color;
        }
    }
}
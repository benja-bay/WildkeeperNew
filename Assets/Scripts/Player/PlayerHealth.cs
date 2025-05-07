// ==============================
// PlayerHealth.cs
// Extends Health.cs to provide flashing visual feedback and HUD update
// ==============================

using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        // === Flash Feedback Configuration ===
        [Header("Flash settings")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashDuration = 0.1f;
        private Color _originalColor;

        // === Custom Regeneration Method ===
        public void Regenerate(int amount)
        {
            Heal(amount);
        }

        // === Damage Handler with Visual Feedback ===
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            StartCoroutine(FlashRed());
        }

        // === Flash Coroutine for Hit Feedback ===
        private System.Collections.IEnumerator FlashRed()
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = _originalColor;
        }

        // === Extended Death Logic for Player ===
        public override void Die()
        {
            base.Die();
            // TODO: Player die event.
        }

        // === Initialization of Flash Settings ===
        private void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            _originalColor = spriteRenderer.color;
        }

        // === HUD Update Per Frame ===
        private void Update()
        {
            HudHealth(); 
        }

        private void HudHealth()
        {
            GameManager.Instance.ShowHealth(_currentHealth);
        }
    }
}
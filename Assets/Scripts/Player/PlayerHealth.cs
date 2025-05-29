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
        [SerializeField] private SpriteRenderer spriteRenderer; // Referencia al sprite del jugador
        [SerializeField] private Color damageColor = Color.red;  // Color para mostrar al recibir daño
        [SerializeField] private float flashDuration = 0.1f;    // Duración del flash al recibir daño

        [SerializeField] private Color healColor = Color.green; // Color para mostrar al curarse
        [SerializeField] private float healDuration = 0.3f;     // Duración del flash al curarse

        private Color _originalColor; // Almacena el color original para restaurarlo

        // === Método de regeneración de vida ===
        public void Regenerate(int amount)
        {
            Heal(amount);                 // Aumenta la vida
            StartCoroutine(FlashGreen()); // Efecto visual de curación
        }

        // === Manejo de daño con efecto visual ===
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);    // Aplica el daño
            StartCoroutine(FlashRed()); // Efecto visual de daño
        }

        // === Corrutina para parpadear en rojo al recibir daño ===
        private System.Collections.IEnumerator FlashRed()
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = _originalColor;
        }

        // === Corrutina para parpadear en verde al curarse ===
        private System.Collections.IEnumerator FlashGreen()
        {
            spriteRenderer.color = healColor;
            yield return new WaitForSeconds(healDuration);
            spriteRenderer.color = _originalColor;
        }

        // === Lógica extendida de muerte del jugador ===
        public override void Die()
        {
            base.Die();
            // TODO: Agregar lógica adicional como animación de muerte, reinicio, etc.
        }

        // === Inicialización del color original ===
        private void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            _originalColor = spriteRenderer.color;
        }

        // === Actualización del HUD cada frame ===
        private void Update()
        {
            HudHealth(); 
        }

        //#TEST
        private void HudHealth()
        {
            GameManager.Instance.ShowHealth(_currentHealth);
        }
    }
}
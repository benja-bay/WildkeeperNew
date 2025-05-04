using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health // player que hereda de health
    {
        [Header("Flash settings")]
        [SerializeField] private SpriteRenderer spriteRenderer; // El sprite del jugador
        [SerializeField] private Color flashColor = Color.red; // Color del flash
        [SerializeField] private float flashDuration = 0.1f; // Duración del flash
        private Color _originalColor; // Guarda el color original del sprite
        
        public void Regenerate(int amount)
        {
            Heal(amount); // regenera usando el heal del padre
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            // Add player-specific feedback, e.g. red flash or animation
            StartCoroutine(FlashRed()); // Inicia el efecto de flash
        }
        private System.Collections.IEnumerator FlashRed()
        {
            spriteRenderer.color = flashColor; // Cambia a color rojo
            yield return new WaitForSeconds(flashDuration); // Espera
            spriteRenderer.color = _originalColor; // Restaura el color original
        }

        public override void Die()
        {
            base.Die();
            // TODO: Player die event.
        }
        
        private void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>(); // Intenta obtenerlo si no fue asignado

            _originalColor = spriteRenderer.color; // Guarda el color original al iniciar
        }

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

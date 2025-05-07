using UnityEngine;

namespace Enemys
{
    public class EnemyHealthDebug : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Health _health;
    
        public Color colorStart = Color.white;
        public Color colorEnd = Color.red;
    
        [Range(0, 100)]
        public int percentage = 0;
    
        void Awake()
        {
            // Obtener el componente SpriteRenderer del GameObject
            _spriteRenderer = GetComponent<SpriteRenderer>();
            // Obtiene el componente Health que debe estar en el mismo GameObject.
            _health = GetComponent<Health>();
        
        }

        // Update is called once per frame
        void Update()
        {
            // Calcula el porcentaje de vida restante del enemigo.
            // Se obtiene la vida actual del enemigo y se divide por la vida máxima para normalizarlo
            float t = (float)_health.GetCurrentHealth() / _health.GetMaxHealth();

            // Interpola entre los dos colores según t
            _spriteRenderer.color = Color.Lerp(colorStart, colorEnd, t);
        }
    }
}

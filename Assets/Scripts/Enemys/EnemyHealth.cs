namespace Enemy
{
    // Controla la salud del enemigo, hereda de una clase base llamada Health
    public class EnemyHealth : Health
    {
        public override void TakeDamage(int amount)
        {
            // Aplica daño normalmente (podrías extender esto más adelante)
            base.TakeDamage(amount);
        }

        public override void Die()
        {
            // Llama la lógica de muerte y destruye el GameObject del enemigo
            base.Die();
            Destroy(gameObject);
        }
    }
}

namespace Enemies
{
    // Controla la salud del enemigo, hereda de una clase base llamada Health
    public class EnemyHealth : Health
    {
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            // Notificar al EnemyController para que actualice el estado si es necesario
            EnemyController controller = GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.UpdateBehaviorStates();
            }
        }

        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}

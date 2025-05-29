// #TEST

using Systems;

namespace Enemys
{
    public class EnemyHealth : Health
    {
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
        }
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}

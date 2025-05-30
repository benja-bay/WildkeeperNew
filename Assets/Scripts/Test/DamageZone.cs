// ==============================
// #TEST
// ==============================
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player.PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
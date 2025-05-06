using Player;
using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    private Transform _player;
    private PlayerInputHandler _inputHandler;
    private Player.Player player;

    [SerializeField] private float _distance = 1f;
    public int damage = 1;

    // Se debe llamar desde el estado antes de usar el hitbox
    public void Initialize(Player.Player playerRef, PlayerInputHandler inputHandler, Transform playerTransform)
    {
        player = playerRef;
        _inputHandler = inputHandler;
        _player = playerTransform;
    }

    public void UpdatePositionAndRotation()
    {
        if (_inputHandler == null || _player == null)
        {
            Debug.LogError("MeleeAttackHitbox: Referencias no inicializadas. Llama Initialize() antes de usar.");
            return;
        }

        Vector2 direction = _inputHandler.mouseDirection;
        transform.position = _player.position + (Vector3)(direction * _distance);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player == null || !player.isAttacking) return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            Debug.Log("Damage was caused to the enemy");
        }
    }
}

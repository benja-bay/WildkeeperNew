// ==============================
// MeleeAttackHitbox.cs
// Manages position, rotation, and collision detection of the melee hitbox
// ==============================

using UnityEngine;
using Player;

public class MeleeAttackHitbox : MonoBehaviour
{
    // === References ===
    private Transform _player; // Reference to the player's transform
    private PlayerInputHandler _inputHandler; // Reference to the player's input handler
    private Player.Player player; // Reference to the Player script

    // === Configuration ===
    [SerializeField] private float _distance = 1f; // Distance from the player to place the hitbox
    public int damage = 1; // Damage dealt by the hitbox

    // === Initialization ===
    public void Initialize(Player.Player playerRef, PlayerInputHandler inputHandler, Transform playerTransform)
    {
        player = playerRef;
        _inputHandler = inputHandler;
        _player = playerTransform;
    }

    // === Updates hitbox position and rotation to match the direction of the mouse ===
    public void UpdatePositionAndRotation()
    {
        if (_inputHandler == null || _player == null)
        {
            Debug.LogError("MeleeAttackHitbox: Referencias no inicializadas. Llama a Initialize() antes de usar.");
            return;
        }

        Vector2 direction = _inputHandler.mouseDirection;
        transform.position = _player.position + (Vector3)(direction.normalized * _distance);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // === Collision Handling ===
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

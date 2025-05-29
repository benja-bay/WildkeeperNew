// ==============================
// MeleeAttackHitbox.cs
// Manages position, rotation, and collision detection of the melee hitbox
// ==============================

using Enemys;
using Objects;
using UnityEngine;

namespace Player
{
    public enum HitboxMode
    {
        kAttack,    // Melee attack mode
        kInteract,  // Interaction mode
        kShoot      // (Reserved) Ranged or projectile logic mode
    }

    public class Hitbox : MonoBehaviour
    {
        // === References ===
        private Transform _playerTransform; // Reference to the player's transform
        private PlayerInputHandler _inputHandler; // Reference to the player's input handler
        private global::Player.Player _player; // Reference to the Player script

        // === Configuration ===
        [SerializeField] private float _distance = 1f; // Distance from the player to place the hitbox
        public int damage = 1; // Damage dealt by the hitbox

        private HitboxMode _mode = HitboxMode.kAttack; // Current operating mode of the hitbox

        // === Initialization ===
        public void Initialize(global::Player.Player playerRef, PlayerInputHandler inputHandler, Transform playerTransform)
        {
            // Initialize references from the player
            _player = playerRef;
            _inputHandler = inputHandler;
            _playerTransform = playerTransform;
        }

        public void SetMode(HitboxMode mode)
        {
            // Change the hitbox behavior based on current interaction type
            _mode = mode;
        }

        // === Updates hitbox position and rotation to match the direction of the mouse ===
        public void UpdatePositionAndRotation()
        {
            if (_inputHandler == null || _playerTransform == null)
            {
                Debug.LogError("MeleeAttackHitbox: References not initialized. Call Initialize() before using.");
                return;
            }

            // Calculate direction based on mouse input and position hitbox accordingly
            Vector2 direction = _inputHandler.MouseDirection;
            transform.position = _playerTransform.position + (Vector3)(direction.normalized * _distance);

            // Rotate hitbox to face the mouse direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Flip the hitbox vertically if facing left
            Vector3 localScale = Vector3.one;
            localScale.y = (angle > 90 || angle < -90) ? -1f : 1f;
            transform.localScale = localScale;
        }

        // === Collision Handling ===
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_player == null) return;

            switch (_mode)
            {
                case HitboxMode.kAttack:
                    // Handle melee attack collision
                    if (!_player.isAttacking) return;

                    if (other.CompareTag("Enemy"))
                    {
                        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(damage);
                            Debug.Log("Damage was caused to the enemy");
                        }
                    }
                    break;

                case HitboxMode.kInteract:
                    // Handle interaction with objects
                    if (!_player.isInteracting) return;

                    if (other.CompareTag("Interactable"))
                    {
                        var interactable = other.GetComponent<IInteractable>();
                        if (interactable != null)
                        {
                            interactable.Interact(_player);
                            Debug.Log("Interaction triggered");
                        }
                    }
                    break;

                case HitboxMode.kShoot:
                    // Reserved for future shoot-based hitbox logic
                    if (!_player.isShooting) return;
                    break;
            }
        }

    }
}
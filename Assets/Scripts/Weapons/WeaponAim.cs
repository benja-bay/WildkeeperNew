// ==============================
// WeaponAim.cs
// Controls weapon position and rotation to follow mouse direction
// ==============================

using UnityEngine;

namespace Weapons
{
    public class WeaponAim : MonoBehaviour
    {
        // === References ===
        [SerializeField] private Transform _player; // Reference to the player's transform

        // === Configuration ===
        [SerializeField] private float _distance; // Distance from player to weapon (e.g., for aiming)

        private void Update()
        {
            // Update weapon aim and position every frame
            UpdatePositionAndRotation();
        }

        // Calculates direction to mouse, rotates the weapon, and positions it accordingly
        public void UpdatePositionAndRotation()
        {
            // Get direction from weapon to mouse position in screen space
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Apply rotation
            transform.eulerAngles = new Vector3(0, 0, angle);

            // Calculate direction from player to mouse in world space
            Vector3 playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _player.position;
            playerToMouseDir.z = 0;

            // Position the weapon at a fixed distance in that direction
            transform.position = _player.position + (_distance * playerToMouseDir.normalized);

            // Flip weapon vertically if aiming left
            Vector3 localScale = Vector3.one;
            localScale.y = (angle > 90 || angle < -90) ? -1f : 1f;
            transform.localScale = localScale;
        }
    }
}

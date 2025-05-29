// ==============================
// WeaponScript.cs
// Handles weapon firing logic including fire rate and bullet instantiation
// ==============================

using UnityEngine;

namespace Weapons
{
    public class WeaponScript : MonoBehaviour
    {
        // === Weapon Configuration ===
        [SerializeField] private Transform _barrel; // Bullet spawn point
        [SerializeField] private GameObject _bullet; // Bullet prefab to instantiate
        [SerializeField] private float _fireRate; // Time delay between each shot

        // === Internal Timer ===
        private float _fireTimer; // Timer to control fire rate
        
        // Fires a bullet from the barrel position and resets the fire timer
        public void Shoot()
        {
            _fireTimer = Time.time + _fireRate;
            Instantiate(_bullet, _barrel.position, _barrel.rotation);
        }

        // Returns true if the weapon is ready to shoot again
        public bool CanShoot()
        {
            return Time.time > _fireTimer;
        }
    }
}

// ==============================
// PlayerInputHandler.cs
// Handles all player input including movement, attack, and mouse direction
// ==============================

using UnityEngine;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        // === Public Input States ===
        public Vector2 movementInput; // Directional movement input from player
        public bool attackPressed; // Whether the attack input was pressed
        public bool interactPressed; // Whether the interact input was pressed
        public bool shootingPressed;
        public Vector2 mouseDirection { get; private set; } // Direction from player to mouse position

        // === Required References ===
        [SerializeField] private Transform _playerTransform; // Transform of the player
        [SerializeField] private Camera _camera; // Camera used to convert screen to world position

        void Update()
        {
            // === Capture Movement & Attack Input ===
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            attackPressed = Input.GetButtonDown("Attack");
            interactPressed = Input.GetButtonDown("Interact");
            shootingPressed = Input.GetKeyDown(KeyCode.J);
            
            // === Update Mouse Direction Vector ===
            UpdateMouseDirection();
        }

        private void UpdateMouseDirection()
        {
            // === Convert Mouse Position to World and Calculate Direction from Player ===
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            Vector3 dir = (mouseWorldPos - _playerTransform.position).normalized;
            mouseDirection = new Vector2(dir.x, dir.y);
        }
    }
}

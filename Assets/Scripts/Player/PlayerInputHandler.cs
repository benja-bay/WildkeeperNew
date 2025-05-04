using UnityEngine;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 movementInput; // public variable to store the movement input
        public bool attackPressed; 
        public Vector2 mouseDirection { get; private set; } // Dirección del mouse desde el jugador
        
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Camera _camera;

        void Update()
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get raw input from keyboard and normalize it to prevent diagonal speed boost
            attackPressed = Input.GetButtonDown("Attack");
            UpdateMouseDirection();
        }
        
        private void UpdateMouseDirection()
        {
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            Vector3 dir = (mouseWorldPos - _playerTransform.position).normalized;
            mouseDirection = new Vector2(dir.x, dir.y);
        }
    }
}

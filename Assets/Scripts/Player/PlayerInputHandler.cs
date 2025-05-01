using UnityEngine;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 movementInput; // public variable to store the movement input

        void Update()
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get raw input from keyboard and normalize it to prevent diagonal speed boost
            
        }
    }
}

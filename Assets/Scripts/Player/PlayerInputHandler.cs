using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    
    public Vector2 movementInput; // Public variable to store the movement input

    void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        // Get raw input from keyboard and normalize it to prevent diagonal speed boost
    }
}

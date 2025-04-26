using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f; // How fast the player moves

    private PlayerInputHandler inputHandler; // Handles player input
    private Rigidbody2D rb; // Controls physics movement

    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>(); // Get the input handler component
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void FixedUpdate()
    {
        Move(inputHandler.movementInput); // Move the player using input
    }

    private void Move(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed; // Set the velocity to move the player
    }
}

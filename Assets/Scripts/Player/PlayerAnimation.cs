using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation
{
    private Animator animator;

    public PlayerAnimation(Animator anim)
    {
        animator = anim;
    }

    public void PlayIdle()
    {
        // Set IsMoving to false to trigger idle animation
        animator.SetBool("IsMoving", false);
    }

    public void PlayWalk(Vector2 movementInput)
    {
        // Set movement parameters for blend tree and trigger walking animation
        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveX", movementInput.x);
        animator.SetFloat("MoveY", movementInput.y);
    }
}

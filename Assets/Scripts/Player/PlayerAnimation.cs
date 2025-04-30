using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private static int _isMoving = Animator.StringToHash("IsMoving");
        private static int _moveX = Animator.StringToHash("MoveX");
        private static int _moveY = Animator.StringToHash("MoveY");
        private Animator _animator;

        public PlayerAnimation(Animator anim)
        {
            _animator = anim;
        }

        public void PlayIdle()
        {
            // Set IsMoving to false to trigger idle animation
            _animator.SetBool(_isMoving, false);
        }

        public void PlayWalk(Vector2 movementInput)
        {
            // Set IsMoving to true to enable the walk animation
            _animator.SetBool(_isMoving, true);
            // Set movement parameters for blend tree
            _animator.SetFloat(_moveX, movementInput.x);
            _animator.SetFloat(_moveY, movementInput.y);
        }
    }
}

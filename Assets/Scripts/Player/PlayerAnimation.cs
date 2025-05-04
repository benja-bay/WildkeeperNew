using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private static int _isMoving = Animator.StringToHash("IsMoving");
        private static int _moveX = Animator.StringToHash("MoveX");
        private static int _moveY = Animator.StringToHash("MoveY");
        private static int _isAttacking = Animator.StringToHash("IsAttacking");
        private static int _attackX = Animator.StringToHash("AttackX");
        private static int _attackY = Animator.StringToHash("AttackY");
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
        
        // nueva animcion PlayMeleAtack en la que:
        public void PlayMeleeAttack(Vector2 mouseDirection)
        {
            _animator.SetBool(_isMoving, false);
            _animator.SetBool("IsAttacking", true);
            _animator.SetFloat("AttackX", mouseDirection.x);
            _animator.SetFloat("AttackY", mouseDirection.y);
            
            // Esto asegura que se realice la transición con blending
            _animator.CrossFade("Attack", 0.1f);
        }

        public void StopMeleeAttack()
        {
            _animator.SetBool(_isAttacking, false);
        }
        
    }
}

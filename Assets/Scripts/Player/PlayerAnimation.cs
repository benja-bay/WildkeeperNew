// ==============================
// PlayerAnimation.cs
// Manages animation transitions for idle, walking, and melee attack
// ==============================

using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        // === Animation Parameter Hashes ===
        private static int _isMoving = Animator.StringToHash("IsMoving");
        private static int _moveX = Animator.StringToHash("MoveX");
        private static int _moveY = Animator.StringToHash("MoveY");
        private static int _isAttacking = Animator.StringToHash("IsAttacking");
        private static int _attackX = Animator.StringToHash("AttackX");
        private static int _attackY = Animator.StringToHash("AttackY");

        private Animator _animator; // Animator reference for controlling animations

        public PlayerAnimation(Animator anim)
        {
            _animator = anim;
        }

        // === Triggers Idle Animation ===
        public void PlayIdle()
        {
            _animator.SetBool(_isMoving, false);
        }

        // === Triggers Walk Animation and Updates Movement Direction ===
        public void PlayWalk(Vector2 movementInput)
        {
            _animator.SetBool(_isMoving, true);
            _animator.SetFloat(_moveX, movementInput.x);
            _animator.SetFloat(_moveY, movementInput.y);
        }

        // === Triggers Melee Attack Animation in Direction of Mouse ===
        public void PlayMeleeAttack(Vector2 mouseDirection)
        {
            _animator.SetBool(_isMoving, false);
            _animator.SetBool("IsAttacking", true);
            _animator.SetFloat("AttackX", mouseDirection.x);
            _animator.SetFloat("AttackY", mouseDirection.y);

            _animator.CrossFade("Attack", 0.1f);
        }

        // === Resets Attack State to Stop Melee Animation ===
        public void StopMeleeAttack()
        {
            _animator.SetBool(_isAttacking, false);
        }
    }
}

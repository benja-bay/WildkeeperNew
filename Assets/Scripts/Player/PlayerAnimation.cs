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
        
        // nueva animcion PlayMeleAtack en la que:
        // cambiamos un booleano que compruebe si esta atacando a true
        // pasamos los datos de la direccion del mouse para con un blend tree controlar la direccion del ataque
        
        // nueva animacion StopMeleAtack en la que:
        // cambiamos un booleano que compruebe si esta atacando a false
        // recordr dejar exit time en el animator para que se reproduzca la animacion complet
    }
}

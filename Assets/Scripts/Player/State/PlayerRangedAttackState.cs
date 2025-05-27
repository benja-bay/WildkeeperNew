using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{
    public class PlayerRangedAttackState : PlayerState
    {
        private readonly WeaponScript _weapon;
        private readonly WeaponAim _aim;

        public PlayerRangedAttackState(Player player, PlayerStateMachine stateMachine,
                                      WeaponScript weapon, WeaponAim aim)
            : base(player, stateMachine)
        {
            _weapon = weapon;
            _aim = aim;
        }

        public override void Enter()
        {
            base.Enter();
            Player.isShooting = true;
            // Ajustar posición y rotación inicial del arma
            _aim.UpdatePositionAndRotation();
            // Intentar disparar de inmediato
            if (_weapon.CanShoot())
                _weapon.Shoot();
        }

        public override void HandleInput()
        {
            // Actualizar orientación del arma según mouse
            _aim.UpdatePositionAndRotation();
        }

        public override void LogicUpdate()
        {
            // Disparar mientras se mantenga pulsada J y respetando cadencia
            if (Player.inputHandler.shootingPressed && _weapon.CanShoot())
            {
                _weapon.Shoot();
            }

            // Al soltar J, volver al estado Idle o Walk según input de movimiento
            if (!Player.inputHandler.shootingPressed)
            {
                if (Player.inputHandler.movementInput != Vector2.zero)
                    StateMachine.ChangeState(Player.WalkState);
                else
                    StateMachine.ChangeState(Player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Player.isShooting = false;
        }
    }
}
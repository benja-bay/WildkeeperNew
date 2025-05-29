using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{
    public class PlayerRangedAttackState : PlayerState
    {
        private readonly WeaponScript _weapon;
        private readonly WeaponAim _aim;
        private bool _unlocked = false;

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
            
            if (!_unlocked)
            {
                Debug.Log("Ataque a distancia no desbloqueado.");
                StateMachine.ChangeState(Player.IdleState);
                return;
            }
            if (!Player.Inventory.HasAmmo(Player.DartItem))
            {
                Debug.Log("Sin munición para disparar.");
                StateMachine.ChangeState(Player.IdleState);
                return;
            }
            
            Player.isShooting = true;
            // Ajustar posición y rotación inicial del arma
            _aim.UpdatePositionAndRotation();
            // Intentar disparar de inmediato
            if (_weapon.CanShoot())
                _weapon.Shoot();
            
            Player.Inventory.ConsumeAmmo(Player.DartItem);
        }

        public override void HandleInput()
        {
            // Actualizar orientación del arma según mouse
            _aim.UpdatePositionAndRotation();
        }

        public override void LogicUpdate()
        {
            // Disparar mientras se mantenga pulsado el boton de atacar y respetando cadencia
            if (Player.inputHandler.attackPressed && _weapon.CanShoot())
            {
                _weapon.Shoot();
            }
            
            // Al soltar el boton de atacar, volver al estado Idle o Walk según input de movimiento
            if (!Player.inputHandler.attackPressed)
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
        
        public void Unlock()
        {
            _unlocked = true;
        }

        public bool IsUnlocked => _unlocked;
        
        
    }
}
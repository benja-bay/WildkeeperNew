using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{ 
    public class PlayerRangedAttackState : PlayerState
    {
        private GameObject _gun;
        public PlayerRangedAttackState(Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox)
            : base(player, stateMachine)
        {
            _gun = meleeHitbox;

        }
    }
}
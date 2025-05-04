using Player;
using Player.State;
using UnityEngine;

public class PlayerMeleAttackState : PlayerState
{
    private GameObject meleeHitbox;
    private MeleeAttackHitbox attackHitbox;
    private float attackDuration = 0.4f;
    private float attackTimer;

    public PlayerMeleAttackState(Player.Player player, PlayerStateMachine stateMachine, GameObject meleeHitbox) 
        : base(player, stateMachine)
    {
        this.meleeHitbox = meleeHitbox;
        this.attackHitbox = meleeHitbox.GetComponent<MeleeAttackHitbox>();
    }

    public override void Enter()
    {
        base.Enter();
        Player.isAttacking = true;
        

        // Inicializamos el hitbox con las referencias necesarias
        attackHitbox.Initialize(Player, Player.GetComponent<PlayerInputHandler>(), Player.transform);
        attackHitbox.UpdatePositionAndRotation();

        Debug.Log("MeleeHitbox activated");
        Player.Move(Vector2.zero); // Stops the player's movement when enter on idle

        meleeHitbox.SetActive(true);
        attackTimer = attackDuration;
        
        Vector2 mouseDirection = Player.GetComponent<PlayerInputHandler>().mouseDirection;
        Player.PlayerAnimation.PlayMeleeAttack(mouseDirection);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        attackTimer -= Time.deltaTime;

        // Mientras ataca, actualiza dirección por si el mouse se mueve
        attackHitbox.UpdatePositionAndRotation();

        if (attackTimer <= 0f)
        {
            if (Player.inputHandler.movementInput != Vector2.zero)
            {
                StateMachine.ChangeState(Player.WalkState);
            }
            else
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        Player.isAttacking = false;
        Player.PlayerAnimation.StopMeleeAttack();
        meleeHitbox.SetActive(false);
    }
}

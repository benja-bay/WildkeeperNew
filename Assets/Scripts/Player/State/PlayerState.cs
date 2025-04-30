namespace Player.State
{
    public abstract class PlayerState
    {
        protected Player Player;
        protected PlayerStateMachine StateMachine;

        protected PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            this.Player = player;
            this.StateMachine = stateMachine;
        }
    
        public virtual void Enter() { } // controls what is executed upon entry
    
        public virtual void Exit() { } // controls what is executed upon exit
    
        // handle input and logic update controls state updates
        public virtual void HandleInput() { }
        public virtual void LogicUpdate() { }
    
        // physics update controls the fixed updates of the state
        public virtual void PhysicsUpdate() { }
    
    }
}
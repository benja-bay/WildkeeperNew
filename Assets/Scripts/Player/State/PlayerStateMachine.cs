using Player.State;

namespace Player
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState; // sets the current state to the state it starts from
            CurrentState.Enter(); // executes the entry of a state
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit(); // executes the exit of a state
            CurrentState = newState; // changes to the new state
            CurrentState.Enter();
        }
    }
}

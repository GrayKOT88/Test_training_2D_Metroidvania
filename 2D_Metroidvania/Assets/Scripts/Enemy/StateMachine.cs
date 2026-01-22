public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
}

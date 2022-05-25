public class StateMachine<T> where T : System.Enum
{
    public IState<T> currentState;

    public bool ApplyState(IState<T> state)
    {
        currentState = state;
        currentState?.OnEnter();
        return true;
    }
}

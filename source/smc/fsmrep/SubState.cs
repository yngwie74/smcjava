namespace SMC.FsmRep
{
    /// <summary>
    /// This interface represents a state
    /// which inherits some transitions from a super state.
    /// </summary>
    public interface SubState : State
    {
        SuperState SuperState { get; }
    }
}
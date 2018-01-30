namespace SMC.FsmRep
{
    /// <summary>
    /// This interface represents a super state.  A super state cannot
    /// be the target of a transition.  It can, however, have transitions
    /// out of itself.  Super states are used by substates which
    /// inherit from them.
    /// </summary>
    public interface SuperState : State
    {
    }
}
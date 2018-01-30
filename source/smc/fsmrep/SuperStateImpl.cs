namespace SMC.FsmRep
{
    /// <summary>
    /// This class represents a super state.  A super state cannot
    /// be the target of a transition.  It can, however, have transitions
    /// out of itself.  Super states are used by substates which
    /// inherit from them.
    /// </summary>
    public class SuperStateImpl : StateImpl, SuperState, State
    {
        public SuperStateImpl(string str)
            : base(str)
        {
        }
    }
}
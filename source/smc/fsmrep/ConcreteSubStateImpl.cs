namespace SMC.FsmRep
{
    /// <summary>
    /// This class represents a substate which is concrete, i.e. is not
    /// also a super state.  Thus it can be the target of a transition.
    /// </summary>
    public class ConcreteSubStateImpl : StateImpl, ConcreteState, State, SubState
    {
        #region Constructors & Destructors

        public ConcreteSubStateImpl(string theName, SuperState ss)
            : base(theName)
        {
            this.SuperState = ss;
        }

        #endregion

        #region Public Properties

        public SuperState SuperState { get; private set; }

        #endregion
    }
}
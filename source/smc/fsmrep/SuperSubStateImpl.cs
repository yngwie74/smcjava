namespace SMC.FsmRep
{
    /// <summary>
    /// This class represents a superstate which is both a super
    /// state and a sub state. Thus it can not be the target of a transition.
    /// </summary>
    public class SuperSubStateImpl : StateImpl, SubState, State, SuperState
    {
        #region Constructors & Destructors

        public SuperSubStateImpl(string str, SuperState ss)
            : base(str)
        {
            this.SuperState = ss;
        }

        #endregion

        #region Public Properties

        public SuperState SuperState { get; private set; }

        #endregion
    }
}
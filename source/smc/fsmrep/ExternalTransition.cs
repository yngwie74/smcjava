namespace SMC.FsmRep
{
    /// <summary>
    /// This class represents a transition from one state to another and
    /// the event that causes this to happen.
    /// </summary>
    public class ExternalTransition : Transition
    {
        #region Constructors & Destructors

        public ExternalTransition(string theEvent,
            State theSourceState, ConcreteState nextState)
            : base(theEvent, theSourceState)
        {
            this.NextState = nextState;
        }

        #endregion

        #region Public Properties

        public ConcreteState NextState { get; private set; }

        #endregion
    }
}
namespace SMC.FsmRep
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a transition from one state to another or
    /// a transition into the same state based upon some event.
    /// </summary>
    public abstract class Transition
    {
        #region Fields

        private IList<string> actions;

        #endregion

        #region Constructors & Destructors

        public Transition(string theEvent, State theSourceState)
        {
            this.Event = theEvent;
            this.SourceState = theSourceState;
            this.actions = new List<string>();
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Actions => this.actions;

        public string Event { get; private set; }

        public State SourceState { get; private set; }

        #endregion

        #region Public Methods

        public void AddAction(string str) => this.actions.Add(str);

        #endregion
    }
}
namespace SMC.FsmRep
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the abstract base class which represents
    /// a state in the finite state machine.
    /// </summary>
    public abstract class StateImpl : State
    {
        #region Fields

        private IList<string> itsEntryActions;
        private IList<string> itsExitActions;
        private ISet<Transition> itsTransitions;

        #endregion

        #region Constructors & Destructors

        public StateImpl(string theName)
        {
            this.Name = theName;
            this.itsTransitions = new HashSet<Transition>();
            this.itsEntryActions = new List<string>();
            this.itsExitActions = new List<string>();
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> EntryActions
        {
            get => this.itsEntryActions;
            set => this.itsEntryActions = new List<string>(value);
        }

        public IEnumerable<string> ExitActions
        {
            get => this.itsExitActions;
            set => this.itsExitActions = new List<string>(value);
        }

        public string Name { get; private set; }

        public IEnumerable<Transition> Transitions => this.itsTransitions;

        #endregion

        #region Public Methods

        public void AddTransition(Transition t) => this.itsTransitions.Add(t);

        #endregion
    }
}
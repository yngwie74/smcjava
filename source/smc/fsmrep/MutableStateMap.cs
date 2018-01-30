namespace SMC.FsmRep
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the structure which holds the
    /// representation of states and transitions.
    /// </summary>
    public class MutableStateMap : StateMap
    {
        #region Fields

        private IList<State> orderedStates;
        private ISet<string> events;
        private ISet<string> actions;
        private List<string> pragma;

        #endregion

        #region Constructors & Destructors

        public MutableStateMap()
        {
            this.orderedStates = new List<State>();
            this.events = new HashSet<string>();
            this.actions = new HashSet<string>();
            this.pragma = new List<string>();
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Actions => this.actions;

        public ConcreteState InitialState { get; set; }

        public IEnumerable<State> OrderedStates => this.orderedStates;

        public IEnumerable<string> Events => this.events;

        public string Name { get; set; }

        public string ContextName { get; set; }

        public IEnumerable<string> Pragma => this.pragma;

        public string Version { get; set; }

        public string ExceptionName { get; set; }

        public string ErrorFunctionName { get; set; }

        public bool UsesExceptions => !string.IsNullOrEmpty(this.ExceptionName);

        #endregion

        #region Public Methods

        public void AddPragma(string theName) => this.pragma.Add(theName);

        public void AddOrderedState(State s)
        {
            // if the state isn't already in itsOrderedStates, add it at the end
            if (!this.orderedStates.Contains(s))
            {
                this.orderedStates.Add(s);
            }
        }

        public void AddTransition(State s, Transition t)
        {
            s.AddTransition(t);
            this.events.Add(t.Event);
            foreach (var action in t.Actions)
            {
                this.actions.Add(action);
            }
        }

        #endregion
    }
}
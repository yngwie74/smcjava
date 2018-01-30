namespace SMC.Builder
{
    using System.Collections.Generic;

    using SMC.Builder.DataModel;
    using SMC.FsmRep;

    /// <summary>
    /// This is the class derived from FSMBuilder which stores the
    /// parsed FSM into the intermediate representation and builds
    /// the final State machine representation
    /// </summary>
    public class FSMRepresentationBuilder : FSMBuilder
    {
        #region Fields

        private bool error;
        private MutableStateMap itsStateMap;
        private string itsInitialState;
        private IList<TransitionRep> itsTransitions;
        private TransitionRep itsCurrentTransition;
        private string itsCurrentState;
        private ISet<string> itsStates;
        private IDictionary<string, StateRep> itsStateReps;
        private IDictionary<string, SuperState> itsSuperStateDictionary;
        private IDictionary<string, ConcreteState> itsConcreteStateDictionary;
        private IDictionary<string, State> itsStateDictionary;

        #endregion

        #region Constructors & Destructors

        public FSMRepresentationBuilder()
        {
            this.itsTransitions = new List<TransitionRep>();
            this.itsStates = new HashSet<string>();
            this.itsStateReps = new Dictionary<string, StateRep>();
            this.itsSuperStateDictionary = new Dictionary<string, SuperState>();
            this.itsConcreteStateDictionary = new Dictionary<string, ConcreteState>();
            this.itsStateDictionary = new Dictionary<string, State>();
            this.error = false;
            this.itsStateMap = new MutableStateMap
            {
                Name = "FSMName",
                ContextName = "FSMContext",
                ExceptionName = "",
                ErrorFunctionName = "FSMError",
                Version = ""
            };
        }

        #endregion

        #region Public Properties

        public StateMap StateMap => this.itsStateMap;

        public IEnumerable<TransitionRep> Transitions => this.itsTransitions;

        public IEnumerable<string> States => this.itsStates;

        #endregion

        #region Public Methods

        public override void SetInitialState(string theName) => this.itsInitialState = theName;

        public override void SetName(string theName) => this.itsStateMap.Name = theName;

        public override void SetContextName(string theName) => this.itsStateMap.ContextName = theName;

        public override void SetException(string theName) => this.itsStateMap.ExceptionName = theName;

        public override void SetVersion(string theVersion) => this.itsStateMap.Version = theVersion;

        public override void AddPragma(string theName) => this.itsStateMap.AddPragma(theName);

        public override void AddSuperSubState(string theName, string theSuperState, SyntaxLocation loc)
            => AddStateRep(new SuperSubStateRep(theName, theSuperState, loc));

        public override void AddSubState(string theName, string theSuperState, SyntaxLocation loc)
            => AddStateRep(new SubStateRep(theName, theSuperState, loc));

        public override void AddSuperState(string theName, SyntaxLocation loc)
            => AddStateRep(new SuperStateRep(theName, loc));

        public override void AddState(string theName, SyntaxLocation loc)
            => AddStateRep(new NormalStateRep(theName, loc));

        public override void AddTransition(string theEvent, string theNextState, SyntaxLocation loc)
        {
            this.itsCurrentTransition = new TransitionRep(this.itsCurrentState, theEvent, theNextState, loc);
            this.itsTransitions.Add(this.itsCurrentTransition);
        }

        public override void AddInternalTransition(string theEvent, SyntaxLocation loc)
        {
            // An internal transition is simply a transition which stays inside
            // its current state.  We are modeling it here as a regular transition
            // whose "theNextState" field is blank.
            AddTransition(theEvent, "", loc);
        }

        public override void AddAction(string theAction)
            => this.itsCurrentTransition.AddAction(theAction);

        public override void AddEntryAction(string theAction)
        {
            var sr = GetStateRep(this.itsCurrentState);
            sr.AddEntryAction(theAction);
        }

        public override void AddExitAction(string theAction)
        {
            var sr = GetStateRep(this.itsCurrentState);
            sr.AddExitAction(theAction);
        }

        public void AddBuiltSuperState(SuperState s)
        {
            this.itsSuperStateDictionary.Add(s.Name, s);
            AddBuiltState(s);
        }

        public void AddBuiltConcreteState(ConcreteState s)
        {
            this.itsConcreteStateDictionary.Add(s.Name, s);
            AddBuiltState(s);
        }

        public StateRep GetStateRep(string theName) => this.itsStateReps[theName];

        public bool IsStateBuilt(string stateName)
            => this.itsStateDictionary.ContainsKey(stateName);

        public State GetBuiltState(string theName) => this.itsStateDictionary[theName];

        public SuperState GetBuiltSuperState(string stateName)
            => this.itsSuperStateDictionary[stateName];

        public ConcreteState GetBuiltConcreteState(string stateName)
            => this.itsConcreteStateDictionary[stateName];

        public override bool Build()
        {
            if (!this.error)
            {
                BuildStateMap();
                if (this.error)
                {
                    Error("Aborting due to inconsistent input.");
                    return false;
                }
                return true;
            }
            Error("Aborting due to semantic errors");
            return false;
        }

        public void SetError() => this.error = true;

        #endregion

        #region Methods

        private void AddStateRep(StateRep theStateRep)
        {
            var name = theStateRep.StateName;
            var stateRep = GetStateRep(name);
            if (stateRep != null)
            {
                if (stateRep != theStateRep)
                {
                    var errorStatement = $"Redefinition of state ({name})";
                    SetError();
                    Error(theStateRep.SyntaxLocation, errorStatement);
                }
            }
            else
            {
                this.itsCurrentState = name;
                this.itsStateReps.Add(name, theStateRep);
                this.itsStates.Add(name);
            }
        }

        private void AddBuiltState(State state)
        {
            this.itsStateDictionary.Add(state.Name, state);
            this.itsStateMap.AddOrderedState(state);
        }

        private void BuildStateMap()
        {
            SetEntryAndExitActions();
            SetTransitions();
            SetInitialState();
        }

        private void SetInitialState()
        {
            var csfound = GetBuiltConcreteState(this.itsInitialState);
            if (csfound != null)
            {
                this.itsStateMap.InitialState = csfound;
            }
            else
            {
                var errorStatement = $"Initial state ({this.itsInitialState}) is not concrete.";
                SetError();
                Error(errorStatement);
            }
        }

        private void SetTransitions()
        {
            foreach (var transitionRep in this.itsTransitions)
            {
                var stateName = transitionRep.StartingState;
                var eventName = transitionRep.EventName;
                var endingStateName = transitionRep.EndingState;

                var stateRep = GetStateRep(stateName);
                if (stateRep != null)
                {
                    if (!stateRep.IsEventBuilt(eventName))
                    {
                        stateRep.AddBuiltEvent(eventName);

                        var concreteState = GetBuiltConcreteState(endingStateName);
                        if ((concreteState != null) || endingStateName == "")
                        {
                            var state = GetBuiltState(stateName);
                            if (state != null)
                            {
                                var transition = (endingStateName != "")
                                    ? new ExternalTransition(eventName, state, concreteState)
                                    : (Transition)new InternalTransition(eventName, state);

                                this.itsStateMap.AddTransition(state, transition);

                                var actions = transitionRep.Actions;
                                foreach (var action in actions)
                                {
                                    transition.AddAction(action);
                                }
                            }
                            else
                            {
                                var errorStatement = $"Cannot build transitions for {stateName} due to previous errors.";
                                Error(errorStatement);
                            }
                        }
                        else
                        {
                            CallError($"Ending state ({endingStateName}) is not a concrete state.", transitionRep);
                        }
                    }
                    else
                    {
                        CallError($"Event ({eventName}) already defined for state ({stateName}).", transitionRep);
                    }
                }
                else
                {
                    CallError($"State ({stateName}) not declared.", transitionRep);
                }
            }
        }

        private void CallError(string errorStatement, TransitionRep transitionRep)
        {
            SetError();
            Error(transitionRep.SyntaxLocation, errorStatement);
        }

        private void SetEntryAndExitActions()
        {
            foreach (var stateName in this.itsStates)
            {
                var stateRep = GetStateRep(stateName);

                if (stateRep != null)
                {
                    var state = stateRep.Build(this);
                    if (state != null)
                    {
                        state.EntryActions = stateRep.EntryActions;
                        state.ExitActions = stateRep.ExitActions;
                    }
                }
            }
        }

        #endregion
    }
}
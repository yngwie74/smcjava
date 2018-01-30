namespace smc.builder
{
    using java.lang;
    using java.util;

    using smc.builder.stateRep;
    using smc.fsmrep;

    public class FSMRepresentationBuilder : FSMBuilder
    {

        private bool itsError;
        private MutableStateMap itsStateMap;
        private string itsInitialState;
        private List itsTransitions;
        private TransitionRep itsCurrentTransition;
        private string itsCurrentState;
        private HashSet itsStates;
        private HashMap itsStateReps;
        private HashMap itsSuperStateDictionary;
        private HashMap itsConcreteStateDictionary;
        private HashMap itsStateDictionary;



        public FSMRepresentationBuilder()
        {
            this.itsStateMap = new MutableStateMap();
            this.itsTransitions = new ArrayList();
            this.itsStates = new HashSet();
            this.itsStateReps = new HashMap();
            this.itsSuperStateDictionary = new HashMap();
            this.itsConcreteStateDictionary = new HashMap();
            this.itsStateDictionary = new HashMap();
            this.itsError = false;
            this.itsStateMap.setName("FSMName");
            this.itsStateMap.setContextName("FSMContext");
            this.itsStateMap.setExceptionName("");
            this.itsStateMap.setErrorFunctionName("FSMError");
            this.itsStateMap.setVersion("");
        }



        public virtual StateMap getStateMap()
        {
            return this.itsStateMap;
        }

        public override void addTransition(string str1, string str2, SyntaxLocation sl)
        {
            this.itsCurrentTransition = new TransitionRep(this.itsCurrentState, str1, str2, sl);
            this.itsTransitions.add(this.itsCurrentTransition);
        }

        public virtual StateRep getStateRep(string str)
        {
            return (StateRep)this.itsStateReps.get(str);
        }

        public virtual void setError()
        {
            this.itsError = true;
        }

        public virtual ConcreteState getBuiltConcreteState(string str)
        {
            return (ConcreteState)this.itsConcreteStateDictionary.get(str);
        }

        public virtual State getBuiltState(string str)
        {
            return (State)this.itsStateDictionary.get(str);
        }

        public virtual List getTransitions()
        {
            return this.itsTransitions;
        }

        public virtual HashSet getStates()
        {
            return this.itsStates;
        }

        public override void setInitialState(string str)
        {
            this.itsInitialState = str;
        }

        public override void setName(string str)
        {
            this.itsStateMap.setName(str);
        }

        public override void setContextName(string str)
        {
            this.itsStateMap.setContextName(str);
        }

        public override void setException(string str)
        {
            this.itsStateMap.setExceptionName(str);
        }

        public override void setVersion(string str)
        {
            this.itsStateMap.setVersion(str);
        }

        public override void addPragma(string str)
        {
            this.itsStateMap.addPragma(str);
        }

        public override void addSuperSubState(string str1, string str2, SyntaxLocation sl)
        {
            this.addStateRep(new SuperSubStateRep(str1, str2, sl));
        }

        public override void addSubState(string str1, string str2, SyntaxLocation sl)
        {
            this.addStateRep(new SubStateRep(str1, str2, sl));
        }

        public override void addSuperState(string str, SyntaxLocation sl)
        {
            this.addStateRep(new SuperStateRep(str, sl));
        }

        public override void addState(string str, SyntaxLocation sl)
        {
            this.addStateRep(new NormalStateRep(str, sl));
        }

        public override void addInternalTransition(string str, SyntaxLocation sl)
        {
            this.addTransition(str, "", sl);
        }

        public override void addAction(string str)
        {
            this.itsCurrentTransition.addAction(str);
        }

        public override void addEntryAction(string str)
        {
            StateRep stateRep = this.getStateRep(this.itsCurrentState);
            stateRep.addEntryAction(str);
        }

        public override void addExitAction(string str)
        {
            StateRep stateRep = this.getStateRep(this.itsCurrentState);
            stateRep.addExitAction(str);
        }

        public virtual void addBuiltSuperState(SuperState ss)
        {
            this.itsSuperStateDictionary.put(ss.getName(), ss);
            this.addBuiltState(ss);
        }

        public virtual void addBuiltConcreteState(ConcreteState cs)
        {
            this.itsConcreteStateDictionary.put(cs.getName(), cs);
            this.addBuiltState(cs);
        }

        public virtual bool isStateBuilt(string str)
        {
            return this.itsStateDictionary.containsKey(str);
        }

        public virtual SuperState getBuiltSuperState(string str)
        {
            return (SuperState)this.itsSuperStateDictionary.get(str);
        }

        public override bool build()
        {
            if (!this.itsError)
            {
                this.buildStateMap();
                if (this.itsError)
                {
                    this.error("Aborting due to inconsistent input.");
                    return false;
                }
                return true;
            }
            this.error("Aborting due to semantic errors");
            return false;
        }



        private void addStateRep(StateRep P_0)
        {
            string stateName = P_0.getStateName();
            StateRep stateRep = this.getStateRep(stateName);
            if (stateRep != null)
            {
                if (stateRep != P_0)
                {
                    string str = new StringBuffer().append("Redefinition of state (").append(stateName).append(")")
                        .ToString();
                    this.setError();
                    this.error(P_0.getSyntaxLocation(), str);
                }
            }
            else
            {
                this.itsCurrentState = stateName;
                this.itsStateReps.put(stateName, P_0);
                this.itsStates.add(stateName);
            }
        }

        private void addBuiltState(State P_0)
        {
            this.itsStateDictionary.put(P_0.getName(), P_0);
            this.itsStateMap.addOrderedState(P_0);
        }

        private void buildStateMap()
        {
            this.setEntryAndExitActions();
            this.setTransitions();
            this.setInitialState();
        }

        private void setEntryAndExitActions()
        {
            Iterator iterator = this.itsStates.iterator();
            while (true)
            {
                if (!iterator.hasNext())
                {
                    break;
                }
                string str = (string)iterator.next();
                StateRep stateRep = this.getStateRep(str);
                if (stateRep != null)
                {
                    State state = stateRep.build(this);
                    if (state != null)
                    {
                        state.setEntryActions(stateRep.getEntryActions());
                        state.setExitActions(stateRep.getExitActions());
                    }
                }
            }
        }

        private void setTransitions()
        {
            int num = 0;
            while (true)
            {
                if (num == this.itsTransitions.size())
                {
                    break;
                }
                TransitionRep transitionRep = (TransitionRep)this.itsTransitions.get(num);
                string startingState = transitionRep.getStartingState();
                string @event = transitionRep.getEvent();
                string endingState = transitionRep.getEndingState();
                StateRep stateRep = this.getStateRep(startingState);
                if (stateRep != null)
                {
                    if (!stateRep.isEventBuilt(@event))
                    {
                        stateRep.addBuiltEvent(@event);
                        ConcreteState builtConcreteState = this.getBuiltConcreteState(endingState);
                        if (builtConcreteState != null || (object)endingState == "")
                        {
                            State builtState = this.getBuiltState(startingState);
                            if (builtState != null)
                            {
                                Transition transition = ((object)endingState == "") ? ((Transition)new InternalTransition(@event, builtState)) : ((Transition)new ExternalTransition(@event, builtState, builtConcreteState));
                                this.itsStateMap.addTransition(builtState, transition);
                                Vector actions = transitionRep.getActions();
                                Iterator iterator = actions.iterator();
                                while (true)
                                {
                                    if (!iterator.hasNext())
                                    {
                                        break;
                                    }
                                    transition.addAction((string)iterator.next());
                                }
                            }
                            else
                            {
                                string str = new StringBuffer().append("Cannot build transitions for ").append(startingState).append(" due to previous errors.")
                                    .ToString();
                                this.error(str);
                            }
                        }
                        else
                        {
                            this.callError(new StringBuffer().append("Ending state (").append(endingState).append(") is not a concrete state.")
                                .ToString(), transitionRep);
                        }
                    }
                    else
                    {
                        this.callError(new StringBuffer().append("Event (").append(@event).append(") already defined for state (")
                            .append(startingState)
                            .append(").")
                            .ToString(), transitionRep);
                    }
                }
                else
                {
                    this.callError(new StringBuffer().append("State (").append(startingState).append(") not declared.")
                        .ToString(), transitionRep);
                }
                num++;
            }
        }

        private void setInitialState()
        {
            ConcreteState builtConcreteState = this.getBuiltConcreteState(this.itsInitialState);
            if (builtConcreteState != null)
            {
                this.itsStateMap.setInitialState(builtConcreteState);
            }
            else
            {
                string str = new StringBuffer().append("Initial state (").append(this.itsInitialState).append(") is not concrete.")
                    .ToString();
                this.setError();
                this.error(str);
            }
        }

        private void callError(string P_0, TransitionRep P_1)
        {
            this.setError();
            this.error(P_1.getSyntaxLocation(), P_0);
        }

    }
}
namespace smc.fsmrep
{
    using System.Collections.Generic;
    //-----------------------------------------------------
    // Name
    //  MutableStateMap
    //
    // Description
    //  This class represents the structure which holds the
    //  representation of states and transitions.  
    //
    public class MutableStateMap : StateMap
    {
        private ConcreteState itsInitialState;
        private List<State> itsOrderedStates;
        private ISet<string> itsEvents;
        private ISet<string> itsActions;
        private string itsName;
        private string itsContextName;
        private string itsExceptionName;
        private string itsErrorFunctionName;
        private string itsVersion;
        private List<string> itsPragma;
        public MutableStateMap()
        {
            itsOrderedStates = new List<State>();
            itsEvents = new HashSet<string>();
            itsActions = new HashSet<string>();
            itsPragma = new List<string>();
        }
        public void setInitialState(ConcreteState s)
        { itsInitialState = s; }
        public void setName(string theName)
        { itsName = theName; }
        public void setContextName(string theName)
        { itsContextName = theName; }
        public void setExceptionName(string theName)
        { itsExceptionName = theName; }
        public void setErrorFunctionName(string theName)
        { itsErrorFunctionName = theName; }
        public void addPragma(string theName)
            { itsPragma.Add(theName); }
        public void setVersion(string theVersion)
        { itsVersion = theVersion; }

        public void addOrderedState(State s)
        {
            // if the state isn't already in itsOrderedStates, add it at the end
            if (!itsOrderedStates.Contains(s) == false)
                itsOrderedStates.Add(s);
        }
        public void addTransition(State s, Transition t)
        {
            s.addTransition(t);
            itsEvents.Add(t.getEvent());
            foreach (var action in t.getActions())
                itsActions.Add(action);
        }

        public ConcreteState getInitialState()
            { return itsInitialState; }
        public IList<State> getOrderedStates()
            { return itsOrderedStates; }
        public ISet<string> getEvents()
            { return itsEvents; }
        public ISet<string> Actions => itsActions;
        public string getName()
            { return itsName; }
        public string getContextName()
            { return itsContextName; }
        public IList<string> getPragma()
            { return itsPragma; }
        public string getVersion()
            { return itsVersion; }
        public string getExceptionName()
            { return itsExceptionName; }
        public string getErrorFunctionName()
            { return itsErrorFunctionName; }
    }
}

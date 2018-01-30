namespace smc.fsmrep
{
    using System.Collections.Generic;
    //using java.lang;
    //using java.util;

    public abstract class StateImpl : State
    {
        #region Fields

        private IList<string> itsEntryActions;
        private IList<string> itsExitActions;
        private string itsName;
        private ISet<Transition> itsTransitions;

        #endregion

        #region Constructors & Destructors

        public StateImpl(string str)
        {
            this.itsName = str;
            this.itsTransitions = new HashSet<Transition>();
            this.itsEntryActions = new List<string>();
            this.itsExitActions = new List<string>();
        }

        #endregion

        #region Public Methods

        public virtual void addTransition(Transition t)
        {
            this.itsTransitions.Add(t);
        }

        public virtual IList<string> getEntryActions()
        {
            return this.itsEntryActions;
        }

        public virtual IList<string> getExitActions()
        {
            return this.itsExitActions;
        }

        public virtual string getName()
        {
            return this.itsName;
        }

        public virtual ISet<Transition> getTransitions()
        {
            return this.itsTransitions;
        }

        public virtual void setEntryActions(IList<string> v)
        {
            this.itsEntryActions = v;
        }

        public virtual void setExitActions(IList<string> v)
        {
            this.itsExitActions = v;
        }

        #endregion
    }
}
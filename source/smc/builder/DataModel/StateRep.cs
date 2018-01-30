namespace smc.builder.stateRep
{
    using System.Collections.Generic;
    using java.lang;
    using java.util;

    using smc.builder;
    using smc.fsmrep;

    public abstract class StateRep
    {
        #region Fields

        private string itsName;
        private SyntaxLocation itsSyntaxLocation;
        private HashSet itsBuiltEvents;
        private IList<string> itsEntryActions;
        private IList<string> itsExitActions;

        #endregion

        #region Constructors & Destructors

        public StateRep(string str, SyntaxLocation sl)
        {
            this.itsName = str;
            this.itsSyntaxLocation = sl;
            this.itsBuiltEvents = new HashSet();
            this.itsEntryActions = new List<string>();
            this.itsExitActions = new List<string>();
        }

        #endregion

        #region Public Methods

        public virtual void addEntryAction(string str)
        {
            this.itsEntryActions.Add(str);
        }

        public virtual void addExitAction(string str)
        {
            this.itsExitActions.Add(str);
        }

        public virtual string getStateName()
        {
            return this.itsName;
        }

        public virtual SyntaxLocation getSyntaxLocation()
        {
            return this.itsSyntaxLocation;
        }

        public virtual bool isEventBuilt(string str)
        {
            return this.itsBuiltEvents.contains(str);
        }

        public virtual void addBuiltEvent(string str)
        {
            this.itsBuiltEvents.add(str);
        }

        public abstract State build(FSMRepresentationBuilder fsmrb);

        public virtual IList<string> getEntryActions() => this.itsEntryActions;

        public virtual IList<string> getExitActions()
        {
            return this.itsExitActions;
        }

        public abstract bool equals(StateRep sr);

        #endregion
    }
}
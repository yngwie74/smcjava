namespace smc.builder.stateRep
{
    using java.lang;
    using java.util;

    using smc.builder;

    public class TransitionRep : Object
    {
        #region Fields

        private string itsStartingState;
        private string itsEvent;
        private string itsEndingState;
        private Vector itsActions;
        private SyntaxLocation itsSyntaxLocation;

        #endregion

        #region Constructors & Destructors

        public TransitionRep(string str1, string str2, string str3, SyntaxLocation sl)
        {
            this.itsStartingState = str1;
            this.itsEvent = str2;
            this.itsEndingState = str3;
            this.itsSyntaxLocation = sl;
            this.itsActions = new Vector();
        }

        #endregion

        #region Public Methods

        public virtual void addAction(string str)
        {
            this.itsActions.Add(str);
        }

        public virtual string getStartingState()
        {
            return this.itsStartingState;
        }

        public virtual string getEvent()
        {
            return this.itsEvent;
        }

        public virtual string getEndingState()
        {
            return this.itsEndingState;
        }

        public virtual Vector getActions()
        {
            return this.itsActions;
        }

        public virtual SyntaxLocation getSyntaxLocation()
        {
            return this.itsSyntaxLocation;
        }

        #endregion
    }
}
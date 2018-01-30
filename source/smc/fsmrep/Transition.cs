namespace smc.fsmrep
{
    using System.Collections.Generic;

    public abstract class Transition
    {
        #region Fields

        private IList<string> itsActions;
        private string itsEvent;
        private State itsSourceState;

        #endregion

        #region Constructors & Destructors

        public Transition(string str, State s)
        {
            this.itsEvent = str;
            this.itsSourceState = s;
            this.itsActions = new List<string>();
        }

        #endregion

        #region Public Methods

        public virtual void addAction(string str)
        {
            this.itsActions.Add(str);
        }

        public virtual IList<string> getActions()
        {
            return this.itsActions;
        }

        public virtual string getEvent()
        {
            return this.itsEvent;
        }

        public virtual State getSourceState()
        {
            return this.itsSourceState;
        }

        #endregion
    }
}
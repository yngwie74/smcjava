namespace SMC.Builder
{
    /// <summary>
    /// This class provides the interface that the FSMParser uses
    /// to declare errors.  The parsing agent is expected to derive its own
    /// implementation of this class.
    /// </summary>
    public abstract class FSMBuilder
    {
        #region Constructors & Destructors

        public FSMBuilder()
        {
        }

        #endregion

        #region Public Properties

        public FSMBuilderErrorManager ErrorManager { private get; set; }

        #endregion

        #region Public Methods

        public abstract void SetName(string theName);

        public abstract void SetContextName(string theName);

        public abstract void SetException(string e);

        public abstract void SetInitialState(string theName);

        public abstract void SetVersion(string theVersion);

        public abstract void AddPragma(string theHeader);

        public abstract void AddSuperSubState(string theName, string theSuperState, SyntaxLocation loc);

        public abstract void AddSuperState(string theName, SyntaxLocation loc);

        public abstract void AddSubState(string theName, string theSuperState, SyntaxLocation loc);

        public abstract void AddState(string theName, SyntaxLocation loc);

        public abstract void AddTransition(string theEvent, string theNextState, SyntaxLocation loc);

        public abstract void AddInternalTransition(string theEvent, SyntaxLocation loc);

        public abstract void AddAction(string theAction);

        public abstract void AddEntryAction(string theAction);

        public abstract void AddExitAction(string theAction);

        public abstract bool Build();

        public void Error(string theString)
        {
            if (this.ErrorManager != null)
            {
                this.ErrorManager.Error(theString);
            }
        }

        public void Error(SyntaxLocation loc, string theString)
        {
            if (this.ErrorManager != null)
            {
                this.ErrorManager.Error(loc, theString);
            }
        }

        #endregion
    }
}
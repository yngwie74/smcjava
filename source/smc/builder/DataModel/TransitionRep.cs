namespace SMC.Builder.DataModel
{
    using System.Collections.Generic;

    using SMC.Builder;

    public class TransitionRep
    {
        #region Fields

        private string startingState;
        private string eventName;
        private string endingState;
        private IList<string> actions;
        private SyntaxLocation syntaxLocation;

        #endregion

        #region Constructors & Destructors

        public TransitionRep(string startState, string eventName, string endState, SyntaxLocation loc)
        {
            this.startingState = startState;
            this.eventName = eventName;
            this.endingState = endState;
            this.syntaxLocation = loc;
            this.actions = new List<string>();
        }

        #endregion

        #region Public Properties

        public string StartingState => this.startingState;

        public string EventName => this.eventName;

        public string EndingState => this.endingState;

        public IEnumerable<string> Actions => this.actions;

        public SyntaxLocation SyntaxLocation => this.syntaxLocation;

        #endregion

        #region Public Methods

        public void AddAction(string a) => this.actions.Add(a);

        #endregion
    }
}
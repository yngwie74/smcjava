namespace SMC.Builder.DataModel
{
    using System.Collections.Generic;

    using SMC.Builder;
    using SMC.FsmRep;

    public abstract class StateRep
    {
        #region Fields

        private string name;
        private SyntaxLocation syntaxLocation;
        private ISet<string> builtEvents;
        private IList<string> entryActions;
        private IList<string> exitActions;

        #endregion

        #region Constructors & Destructors

        public StateRep(string theName, SyntaxLocation loc)
        {
            this.name = theName;
            this.syntaxLocation = loc;
            this.builtEvents = new HashSet<string>();
            this.entryActions = new List<string>();
            this.exitActions = new List<string>();
        }

        #endregion

        #region Public Properties

        public string StateName => this.name;

        public SyntaxLocation SyntaxLocation => this.syntaxLocation;

        public IEnumerable<string> EntryActions => this.entryActions;

        public IEnumerable<string> ExitActions => this.exitActions;

        #endregion

        #region Public Methods

        public void AddBuiltEvent(string theEvent) => this.builtEvents.Add(theEvent);

        public void AddEntryAction(string a) => this.entryActions.Add(a);

        public void AddExitAction(string a) => this.exitActions.Add(a);

        public bool IsEventBuilt(string e) => this.builtEvents.Contains(e);

        public abstract State Build(FSMRepresentationBuilder fb);

        public virtual bool Equals(StateRep s) => (s != null) && (s.StateName == this.StateName);

        public override bool Equals(object obj) => ReferenceEquals(obj, this) || Equals(obj as StateRep);

        public override int GetHashCode()
        {
            var comp = EqualityComparer<string>.Default;
            return -305392441 *
                (-1521134295 + comp.GetHashCode(this.GetType().Name)) *
                (-1521134295 + comp.GetHashCode(this.name));
        }

        public static bool operator ==(StateRep rep1, StateRep rep2)
            => EqualityComparer<StateRep>.Default.Equals(rep1, rep2);

        public static bool operator !=(StateRep rep1, StateRep rep2) => !(rep1 == rep2);

        #endregion
    }
}
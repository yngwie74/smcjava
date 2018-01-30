namespace SMC.Builder.DataModel
{
    using SMC.Builder;
    using SMC.FsmRep;

    public class NormalStateRep : StateRep
    {
        #region Constructors & Destructors

        public NormalStateRep(string theName, SyntaxLocation loc)
            : base(theName, loc)
        {
        }

        #endregion

        #region Public Methods

        public override State Build(FSMRepresentationBuilder fb)
        {
            var retval = new ConcreteStateImpl(this.StateName);
            fb.AddBuiltConcreteState(retval);
            return retval;
        }

        public override bool Equals(StateRep s) => base.Equals(s) && (s is NormalStateRep);

        public override string ToString() => this.StateName;

        #endregion
    }
}
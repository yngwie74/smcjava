namespace SMC.Builder.DataModel
{
    using SMC.Builder;
    using SMC.FsmRep;

    public class SuperStateRep : StateRep
    {
        #region Constructors & Destructors

        public SuperStateRep(string theName, SyntaxLocation loc)
            : base(theName, loc)
        {
        }

        #endregion

        #region Public Methods

        public override State Build(FSMRepresentationBuilder fb)
        {
            // Many other states may depend upon super states.  Thus there may
            // be many requests for them to be built.  Thus we have to check to
            // see if its has already been built, and ignore any subsequent
            // requests.
            //
            // Also, since super states do not depend upon anyone, they can be
            // built as soon as they are seen.

            SuperState retval;
            if (!fb.IsStateBuilt(this.StateName))
            {
                var ss = new SuperStateImpl(this.StateName);
                fb.AddBuiltSuperState(ss);
                retval = ss;
            }
            else
            {
                retval = fb.GetBuiltSuperState(this.StateName);
            }
            return retval;
        }

        public override bool Equals(StateRep s) => base.Equals(s) && (s is SuperStateRep);

        public override string ToString() => $"({this.StateName})";

        #endregion
    }
}
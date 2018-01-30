namespace SMC.Builder.DataModel
{
    using SMC.Builder;
    using SMC.FsmRep;

    public class SuperSubStateRep : StateRep
    {
        #region Constructors & Destructors

        public SuperSubStateRep(string theName, string theSuper, SyntaxLocation loc)
            : base(theName, loc)
        {
            this.SuperStateName = theSuper;
        }

        #endregion

        #region Public Properties

        public virtual string SuperStateName { get; private set; }

        #endregion

        #region Public Methods

        public override State Build(FSMRepresentationBuilder fb)
        {
            // SuperSub states depend upon other states, and so must request
            // that they be built first.  Also, they are depended upon by other
            // states, so they must ignore duplicate requests for being built.

            State retval = null;
            if (!fb.IsStateBuilt(this.StateName))
            {
                var sr = fb.GetStateRep(this.SuperStateName);
                if (sr != null)
                {
                    sr.Build(fb);
                    var superState = fb.GetBuiltSuperState(this.SuperStateName);
                    if (superState != null)
                    {
                        var s = new SuperSubStateImpl(this.StateName, superState);
                        retval = s;
                        fb.AddBuiltSuperState(s);
                    }
                    else
                    {
                        var e = "Could not build super sub state (" + this.StateName + ") because super state (" + this.SuperStateName + ") had an error.";
                        fb.SetError();
                        fb.Error(sr.SyntaxLocation, e);
                    }
                }
                else
                {
                    var e = $"Super state ({this.SuperStateName}) was not declared.";
                    fb.SetError();
                    fb.Error(sr.SyntaxLocation, e);
                }
            }
            else
            {
                retval = fb.GetBuiltState(this.StateName);
            }
            return retval;
        }

        public override bool Equals(StateRep s)
        {
            var other = s as SuperSubStateRep;
            return base.Equals(s) &&
                (other != null) && (other.SuperStateName == this.SuperStateName);
        }

        public override string ToString() => $"({this.StateName}) : {this.SuperStateName}";

        #endregion
    }
}
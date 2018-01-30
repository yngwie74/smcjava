namespace SMC.Builder.DataModel
{
    using SMC.Builder;
    using SMC.FsmRep;

    public class SubStateRep : StateRep
    {
        #region Constructors & Destructors

        public SubStateRep(string theName, string theSuper, SyntaxLocation loc)
            : base(theName, loc)
        {
            this.SuperStateName = theSuper;
        }

        #endregion

        #region Public Properties

        public string SuperStateName { get; private set; }

        #endregion

        #region Public Methods

        public override State Build(FSMRepresentationBuilder fb)
        {
            ConcreteSubStateImpl retval = null;
            var sr = fb.GetStateRep(this.SuperStateName);
            if (sr != null)
            {
                sr.Build(fb);
                SuperState superState = fb.GetBuiltSuperState(this.SuperStateName);
                if (superState != null)
                {
                    retval = new ConcreteSubStateImpl(this.StateName, superState);
                    fb.AddBuiltConcreteState(retval);
                }
                else
                {
                    var e = $"Could not build sub state ({this.StateName}) because super state ({this.SuperStateName}) had an error.";
                    fb.SetError();
                    fb.Error(sr.SyntaxLocation, e);
                }
            }
            else
            {
                var e = $"Super state ({this.SuperStateName}) was not declared.";
                fb.SetError();
                fb.Error(null, e);
            }
            return retval;
        }

        public override bool Equals(StateRep s)
        {
            var other = s as SubStateRep;
            return base.Equals(s) &&
                (other != null) && (other.SuperStateName == this.SuperStateName);
        }

        public override string ToString() => $"{this.StateName}:{this.SuperStateName}";

        #endregion
    }
}
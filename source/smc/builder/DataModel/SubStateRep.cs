namespace smc.builder.stateRep
{
    using java.lang;

    using smc.builder;
    using smc.fsmrep;

    public class SubStateRep : StateRep
    {
        #region Fields

        private string itsSuperStateName;

        #endregion

        #region Constructors & Destructors

        public SubStateRep(string str1, string str2, SyntaxLocation sl)
            : base(str1, sl)
        {
            this.itsSuperStateName = str2;
        }

        #endregion

        #region Public Methods

        public virtual string getSuperStateName()
        {
            return this.itsSuperStateName;
        }

        public override State build(FSMRepresentationBuilder fsmrb)
        {
            ConcreteSubStateImpl concreteSubStateImpl = null;
            StateRep stateRep = fsmrb.getStateRep(this.getSuperStateName());
            if (stateRep != null)
            {
            stateRep.build(fsmrb);
            SuperState builtSuperState = fsmrb.getBuiltSuperState(this.getSuperStateName());
            if (builtSuperState != null)
            {
                concreteSubStateImpl = new ConcreteSubStateImpl(this.getStateName(), builtSuperState);
                fsmrb.addBuiltConcreteState(concreteSubStateImpl);
            }
            else
            {
                string str = new StringBuffer().append("Could not build sub state (").append(this.getStateName()).append(") because super state (")
                    .append(this.getSuperStateName())
                    .append(") had an error.")
                    .ToString();
                fsmrb.setError();
                fsmrb.error(stateRep.getSyntaxLocation(), str);
            }
            }
            else
            {
            string str2 = new StringBuffer().append("Super state (").append(this.getSuperStateName()).append(") was not declared.")
                .ToString();
            fsmrb.setError();
            fsmrb.error(null, str2);
            }
            return concreteSubStateImpl;
        }

        public override bool equals(StateRep sr)
        {
            if ((object)sr.getStateName() == this.getStateName() && sr is SubStateRep)
            {
            SubStateRep subStateRep = (SubStateRep)sr;
            if ((object)subStateRep.getSuperStateName() == this.getSuperStateName())
            {
                return true;
            }
            }
            return false;
        }

        public override string ToString()
        {
            return new StringBuffer().append(this.getStateName()).append(":").append(this.getSuperStateName())
            .ToString();
        }

        #endregion
    }
}
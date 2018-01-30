namespace smc.builder.stateRep
{
    using java.lang;

    using smc.builder;
    using smc.fsmrep;

    public class SuperSubStateRep : StateRep
    {
        #region Fields

        private string itsSuperStateName;

        #endregion

        #region Constructors & Destructors

        public SuperSubStateRep(string str1, string str2, SyntaxLocation sl)
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
            object obj = null;
            if (!fsmrb.isStateBuilt(this.getStateName()))
            {
            StateRep stateRep = fsmrb.getStateRep(this.getSuperStateName());
            if (stateRep != null)
            {
                stateRep.build(fsmrb);
                SuperState builtSuperState = fsmrb.getBuiltSuperState(this.getSuperStateName());
                if (builtSuperState != null)
                {
                    SuperSubStateImpl superSubStateImpl = new SuperSubStateImpl(this.getStateName(), builtSuperState);
                    obj = superSubStateImpl;
                    fsmrb.addBuiltSuperState(superSubStateImpl);
                }
                else
                {
                    string str = new StringBuffer().append("Could not build super sub state (").append(this.getStateName()).append(") because super state (")
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
                fsmrb.error(stateRep.getSyntaxLocation(), str2);
            }
            }
            else
            {
            obj = fsmrb.getBuiltState(this.getStateName());
            }
            object obj2 = obj;
            object obj3;
            if (obj2 != null)
            {
            obj3 = (obj2 as State);
            if (obj3 == null)
            {
                throw new java.lang.IncompatibleClassChangeError();
            }
            }
            else
            {
            obj3 = null;
            }
            return (State)obj3;
        }

        public override bool equals(StateRep sr)
        {
            if ((object)sr.getStateName() == this.getStateName() && sr is SuperSubStateRep)
            {
            SuperSubStateRep superSubStateRep = (SuperSubStateRep)sr;
            if ((object)superSubStateRep.getSuperStateName() == this.getSuperStateName())
            {
                return true;
            }
            }
            return false;
        }

        public override string ToString()
        {
            return new StringBuffer().append("(").append(this.getStateName()).append(") : ")
            .append(this.getSuperStateName())
            .ToString();
        }

        #endregion
    }
}
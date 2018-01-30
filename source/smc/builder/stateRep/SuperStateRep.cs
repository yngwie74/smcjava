namespace smc.builder.stateRep
{
    using java.lang;

    using smc.builder;
    using smc.fsmrep;

    public class SuperStateRep : StateRep
    {
        #region Constructors & Destructors

        public SuperStateRep(string str, SyntaxLocation sl)
            : base(str, sl)
        {
        }

        #endregion

        #region Public Methods

        public override State build(FSMRepresentationBuilder fsmrb)
        {
            object obj;
            if (!fsmrb.isStateBuilt(this.getStateName()))
            {
            SuperStateImpl superStateImpl = new SuperStateImpl(this.getStateName());
            fsmrb.addBuiltSuperState(superStateImpl);
            obj = superStateImpl;
            }
            else
            {
            obj = fsmrb.getBuiltSuperState(this.getStateName());
            }
            object obj2 = obj;
            object obj3 = obj2;
            object obj4;
            if (obj3 != null)
            {
            obj4 = (obj3 as State);
            if (obj4 == null)
            {
                throw new java.lang.IncompatibleClassChangeError();
            }
            }
            else
            {
            obj4 = null;
            }
            return (State)obj4;
        }

        public override bool equals(StateRep sr)
        {
            if ((object)sr.getStateName() == this.getStateName() && sr is SuperStateRep)
            {
            return true;
            }
            return false;
        }

        public override string ToString()
        {
            return new StringBuffer().append("(").append(this.getStateName()).append(")")
            .ToString();
        }

        #endregion
    }
}
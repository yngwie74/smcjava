namespace smc.builder.stateRep
{

    using smc.builder;
    using smc.fsmrep;

    public class NormalStateRep : StateRep
    {
        public NormalStateRep(string theName, SyntaxLocation loc)
          : base(theName, loc)
        {
        }

        public override State build(FSMRepresentationBuilder fb)
        {
            ConcreteStateImpl retval = new ConcreteStateImpl(getStateName());
            fb.addBuiltConcreteState(retval);
            return retval;
        }

        public override bool equals(StateRep s)
        {
            if (s.getStateName() == getStateName() && s is NormalStateRep)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return getStateName();
        }
    }
}
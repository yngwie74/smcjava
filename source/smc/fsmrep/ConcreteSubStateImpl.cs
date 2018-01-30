namespace smc.fsmrep
{
//----------------------------------
// Name
//  ConcreteSubStateImpl
//
// Description
//  This class represents a substate which is concrete, i.e. is not
//  also a super state.  Thus it can be the target of a transition.
//
    public class ConcreteSubStateImpl : StateImpl, ConcreteState, State, SubState
    {
        private SuperState itsSuperState;

        public ConcreteSubStateImpl(string theName, SuperState ss)
          : base( theName )
        {
            itsSuperState = ss;
        }

        public SuperState getSuperState()
        {
            return itsSuperState;
        }
    }
}

namespace smc.fsmrep
{
//----------------------------------
// Name
//  ConcreteStateImpl
//
// Description
//  This class represents a concrete state in the finite
//  state machine.  A concrete state can be the target of
//  a transition.
//
    public class ConcreteStateImpl : StateImpl, ConcreteState, State
    {
        public ConcreteStateImpl(string theName)
            : base(theName)
        {
        }
    }
}

namespace smc.fsmrep
{

//-------------------------------
// Name
//  ExternalTransition
//
// Description
//  This class represents a transition from one state to another and
//  the event that causes this to happen.
//
    public class ExternalTransition : Transition
    {
        private ConcreteState itsNextState;

        public ExternalTransition(string theEvent,
                State theSourceState, ConcreteState nextState)
          : base(theEvent, theSourceState)
        {
            itsNextState = nextState;
        }

        public void setNextState(ConcreteState nextState)
        {
            itsNextState = nextState;
        }

        public ConcreteState getNextState()
        {
            return itsNextState;
        }
    }
}

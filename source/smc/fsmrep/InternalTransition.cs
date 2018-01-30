namespace smc.fsmrep
{
//-------------------------------
// Name
//  InternalTransition
//
// Description
//  This class represents a transition based upon some event, where
//  the end result is to stay in the original state.
//
    public class InternalTransition : Transition
    {
        public InternalTransition(string theEvent, State theSourceState)
          : base(theEvent, theSourceState)
        {
        }
    }
}

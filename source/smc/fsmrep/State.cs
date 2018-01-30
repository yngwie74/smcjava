namespace smc.fsmrep
{
    using System.Collections.Generic;

//----------------------------------
// Name
//  State
//
// Description
//  This is the interface which represents
//  a state in the finite state machine.
//
    public interface State
    {
        string getName();
        ISet<Transition> getTransitions();
        IList<string> getEntryActions();
        IList<string> getExitActions();
        void addTransition(Transition t);
        void setEntryActions(IList<string> v);
        void setExitActions(IList<string> v);
    }
}

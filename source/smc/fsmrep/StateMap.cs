namespace smc.fsmrep
{
    using System.Collections.Generic;

    public interface StateMap
    {
        ISet<string> Actions { get; }

        string getContextName();

        string getErrorFunctionName();

        ISet<string> getEvents();

        string getExceptionName();

        ConcreteState getInitialState();

        string getName();

        IList<State> getOrderedStates();

        IList<string> getPragma();

        string getVersion();
    }
}
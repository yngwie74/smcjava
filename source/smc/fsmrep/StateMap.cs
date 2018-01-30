namespace SMC.FsmRep
{
    using System.Collections.Generic;

    /// <summary>
    /// This interface class represents the structure which holds the
    /// representation of states and transitions.  The parser specifies the
    /// implementation of this class.
    /// </summary>
    public interface StateMap
    {
        /// <summary>
        /// Returns a Set containing Strings representing all the action names.
        /// </summary>
        IEnumerable<string> Actions { get; }

        /// <summary>
        /// Returns the name of the FSM Context.
        /// </summary>
        string ContextName { get; }

        /// <summary>
        /// Returns the name of the Error function to be used by the FSM.
        /// </summary>
        string ErrorFunctionName { get; }

        /// <summary>
        /// Returns a Set containing Strings representing all the event names.
        /// </summary>
        IEnumerable<string> Events { get; }

        /// <summary>
        /// Returns the name of the Exception to be used by the FSM.
        /// </summary>
        string ExceptionName { get; }

        /// <summary>
        /// Returns the State that the FSM starts up in.
        /// </summary>
        ConcreteState InitialState { get; }

        /// <summary>
        /// Returns the name of the FSM.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a sequence of States. The sequence
        /// is ordered by state dependencies.  i.e.
        /// the order guarantees that all substates come
        /// after their super states.
        /// </summary>
        IEnumerable<State> OrderedStates { get; }

        /// <summary>
        /// Returns a sequence of Strings representing all the 
        /// pragma fields that any generated code will be dependent upon.
        /// </summary>
        IEnumerable<string> Pragma { get; }

        /// <summary>
        /// Returns the name of the Version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Determines if an exception class name has been set for this StateMap.
        /// </summary>
        bool UsesExceptions { get; }
    }
}
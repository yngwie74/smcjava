namespace SMC.FsmRep
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface which represents
    /// a state in the finite state machine.
    /// </summary>
    public interface State
    {
        #region Properties

        string Name { get; }

        IEnumerable<Transition> Transitions { get; }

        IEnumerable<string> EntryActions { get; set; }

        IEnumerable<string> ExitActions { get; set; }

        #endregion

        #region Methods

        void AddTransition(Transition t);

        #endregion
    }
}
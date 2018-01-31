namespace SMC.Parser.Iface
{
    public interface SMParserInterface
    {
        /// <summary>Set the name of the Finite State Machine</summary>
        void setFSMName(string s);

        /// <summary>
        /// Set the name of the Context class that the finite state machine
        /// will inherit from
        /// </summary>
        void setContextName(string s);

        /// <summary>
        /// Set the name of the Exception class that is used by the finite state machine
        /// </summary>
        void setException(string s);

        /// <summary>Set the initial state of the FSM</summary>
        void setInitialState(string s);

        /// <summary>Set the version string</summary>
        void setVersion(string s);

        /// <summary>
        /// Set the name of the class that represents the FSM code generator
        /// </summary>
        void setFSMGenerator(string s);

        /// <summary>Add a pragma statement for the FSM code generator</summary>
        void addPragma(string s);

        /// <summary>
        /// The FSM data structures are built, so do whatever needs to be
        /// done to process them.
        /// </summary>
        void processFSM();

        /// <summary>Add a SuperSubState to the FSM</summary>
        void addSuperSubState(string name, string sup, int lNum);

        /// <summary>Add A SuperState to the FSM</summary>
        void addSuperState(string name, int lNum);

        /// <summary>Add A SubState to the FSM</summary>
        void addSubState(string name, string sup, int lNum);

        /// <summary>Add a regular state to the FSM</summary>
        void addState(string name, int lNum);

        /// <summary>Add a transition to the FSM</summary>
        void addTransition(string _event, string nextState, int lNum);

        /// <summary>Add an internal transition to the FSM</summary>
        void addInternalTransition(string _event, int lNum);

        /// <summary>
        /// Add an action to the last transition that was added to the FSM
        /// </summary>
        void addAction(string action, int lNum);

        /// <summary>
        /// Add an action to the list of entry actions for the current state.
        /// </summary>
        void addEntryAction(string action, int lNum);

        /// <summary>
        /// Add an action to the list of exit actions for the current state.
        /// </summary>
        void addExitAction(string action, int lNum);
    }
}

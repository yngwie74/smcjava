namespace SMC.Builder
{
    using System.Linq;

    using NUnit.Framework;

    using SMC.FsmRep;
    using SMC.Parser;

    public class FSMRepresentationBuilderTest
    {
        #region Fields

        private FSMRepresentationBuilder fsm;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            this.fsm = new FSMRepresentationBuilder();
        }

        [Test]
        public void StateName()
        {
            this.fsm.AddState("StateName", new ParserSyntaxLocation("", 0));
            this.fsm.Build();
            Assert.AreEqual("StateName", this.fsm.GetBuiltState("StateName").Name);
        }

        [Test]
        public void Pragmas()
        {
            this.fsm.AddPragma("pragma prag0");
            this.fsm.AddPragma("pragma prag1");
            this.fsm.AddPragma("pragma prag2");
            this.fsm.AddPragma("pragma prag3");
            this.fsm.Build();
            Assert.AreEqual("pragma prag0", this.fsm.StateMap.Pragma.ElementAt(0));
            Assert.AreEqual("pragma prag1", this.fsm.StateMap.Pragma.ElementAt(1));
            Assert.AreEqual("pragma prag2", this.fsm.StateMap.Pragma.ElementAt(2));
            Assert.AreEqual("pragma prag3", this.fsm.StateMap.Pragma.ElementAt(3));
        }

        [Test]
        public void BuildStates()
        {
            InitBuildStates();
            this.fsm.Build();
            Assert.AreEqual(4, this.fsm.StateMap.OrderedStates.Count());
        }

        [Test]
        public void RetreiveStates()
        {
            InitBuildStates();
            this.fsm.Build();
            var states = this.fsm.StateMap.OrderedStates.ToArray();
            var state4Name = states[3].Name;
            var state1Name = states[0].Name;
            Assert.AreEqual("State4", state4Name);
            Assert.AreEqual("State1", state1Name);
        }

        [Test]
        public void BuildTransition()
        {
            InitBuildStates();
            this.fsm.AddTransition("Event", "State4", null);
            this.fsm.Build();

            var state = FindStateNamed("State4", this.fsm.StateMap);
            Assert.AreEqual(1, state.Transitions.Count());
        }

        [Test]
        public void BuildManyTransition()
        {
            InitBuildStates();
            this.fsm.AddTransition("Event", "State4", null);
            this.fsm.AddTransition("Event2", "State4", null);
            this.fsm.AddTransition("Event3", "State4", null);
            this.fsm.AddTransition("Event4", "State4", null);
            this.fsm.Build();

            var state = FindStateNamed("State4", this.fsm.StateMap);
            Assert.AreEqual(4, state.Transitions.Count());
        }

        [Test]
        public void BuildConcreteStates()
        {
            this.fsm.AddBuiltConcreteState(new ConcreteStateImpl("ConcreteState1"));
            this.fsm.Build();
            Assert.IsNotNull(this.fsm.GetBuiltConcreteState("ConcreteState1"));
            Assert.IsNull(this.fsm.GetBuiltConcreteState("ConcreteStat0"));
        }

        [Test]
        public void BuildSuperStates()
        {
            this.fsm.AddBuiltSuperState(new SuperStateImpl("SuperState"));
            this.fsm.Build();
            Assert.IsNotNull(this.fsm.GetBuiltSuperState("SuperState"));
            Assert.IsNull(this.fsm.GetBuiltSuperState("SuperState12"));
        }

        [Test]
        public void BuildEntryAction()
        {
            this.fsm.AddState("State", null);
            this.fsm.AddEntryAction("EntryAction");
            this.fsm.Build();
            Assert.AreEqual(1, this.fsm.GetBuiltState("State").EntryActions.Count());
            Assert.AreEqual("EntryAction", this.fsm.GetBuiltState("State").EntryActions.ElementAt(0));
        }

        [Test]
        public void BuildManyEntryActions()
        {
            this.fsm.AddState("State", null);
            this.fsm.AddEntryAction("EntryAction");
            this.fsm.AddEntryAction("EntryAction2");
            this.fsm.AddEntryAction("EntryAction3");
            this.fsm.AddEntryAction("EntryAction4");
            this.fsm.Build();
            Assert.AreEqual(4, this.fsm.GetBuiltState("State").EntryActions.Count());
        }

        [Test]
        public void BuildExitAction()
        {
            this.fsm.AddState("State", null);
            this.fsm.AddExitAction("ExitAction");
            this.fsm.Build();
            Assert.AreEqual(1, this.fsm.GetBuiltState("State").ExitActions.Count());
            Assert.AreEqual("ExitAction", this.fsm.GetBuiltState("State").ExitActions.ElementAt(0));
        }

        [Test]
        public void BuildManyExitActions()
        {
            this.fsm.AddState("State", null);
            this.fsm.AddExitAction("ExitAction");
            this.fsm.AddExitAction("ExitAction2");
            this.fsm.AddExitAction("ExitAction3");
            this.fsm.AddExitAction("ExitAction4");
            this.fsm.Build();
            Assert.AreEqual(4, this.fsm.GetBuiltState("State").ExitActions.Count());
        }

        [Test]
        public void SuperAndSupStates()
        {
            this.fsm.AddSuperState("SuperState", null);
            this.fsm.AddSubState("SubState", "SuperState", null);
            this.fsm.Build();
            Assert.AreEqual("SuperState", this.fsm.StateMap.OrderedStates.ElementAt(0).Name);
            Assert.AreEqual("SubState", this.fsm.StateMap.OrderedStates.ElementAt(1).Name);
        }

        [Test]
        public void ContextName()
        {
            this.fsm.SetContextName("ContextName");
            this.fsm.Build();
            Assert.AreEqual("ContextName", this.fsm.StateMap.ContextName);
        }

        [Test]
        public void Errors()
        {
            this.fsm.SetError();
            this.fsm.Build();
            Assert.AreEqual("FSMError", this.fsm.StateMap.ErrorFunctionName);
        }

        [Test]
        public void Exceptions()
        {
            this.fsm.SetException("Exception");
            this.fsm.Build();
            Assert.AreEqual("Exception", this.fsm.StateMap.ExceptionName);
        }

        [Test]
        public void InitialState()
        {
            this.fsm.AddState("InitialState", null);
            this.fsm.SetInitialState("InitialState");
            this.fsm.Build();
            Assert.AreEqual("InitialState", this.fsm.StateMap.InitialState.Name);
        }

        [Test]
        public void Name()
        {
            this.fsm.SetName("Name");
            this.fsm.Build();
            Assert.AreEqual("Name", this.fsm.StateMap.Name);
        }

        [Test]
        public void Version()
        {
            this.fsm.SetVersion("Version");
            this.fsm.Build();
            Assert.AreEqual("Version", this.fsm.StateMap.Version);
        }

        [Test]
        public void SetTransitionsError()
        {
            var errorManager = new ParserErrorManager();
            this.fsm.ErrorManager = errorManager;
            this.fsm.Build();
            Assert.AreEqual(2, errorManager.Errors.Count());
        }

        [Test]
        public void NoInitialState_SetTransitionsErrorTypes()
        {
            var errorManager = new ParserErrorManager();
            this.fsm.ErrorManager = errorManager;
            this.fsm.Build();

            var errors = errorManager.Errors.ToArray();
            Assert.AreEqual("Initial state (null) is not concrete.", errors[0]);
            Assert.AreEqual("Aborting due to inconsistent input.", errors[1]);
        }

        [Test]
        public void SetDuplicantEventsTypes()
        {
            var errorManager = new ParserErrorManager();
            this.fsm.ErrorManager = errorManager;
            this.fsm.AddBuiltConcreteState(new ConcreteStateImpl("State1"));
            this.fsm.AddBuiltConcreteState(new ConcreteStateImpl("State2"));
            this.fsm.SetError();
            this.fsm.SetInitialState("State1");
            this.fsm.AddTransition("Event", "State2", null);
            this.fsm.AddTransition("Event", "State2", null);
            this.fsm.AddTransition("Event", "State2", null);
            this.fsm.AddTransition("Event", "State2", null);
            Assert.IsFalse(this.fsm.Build());
            Assert.AreEqual(1, errorManager.Errors.Count());
        }

        #endregion

        #region Helper Methods

        private State FindStateNamed(string stateName, StateMap stateMap) => stateMap.OrderedStates.Single(s => s.Name == stateName);

        private void InitBuildStates()
        {
            this.fsm.AddState("State1", null);
            this.fsm.AddState("State2", null);
            this.fsm.AddState("State3", null);
            this.fsm.AddState("State4", null);
        }

        #endregion
    }
}
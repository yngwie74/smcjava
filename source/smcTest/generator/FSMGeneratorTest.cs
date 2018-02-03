namespace SMC.Generator
{
    using System.Linq;

    using NUnit.Framework;

    using SMC.Builder;
    using SMC.FsmRep;
    using SMC.Generator.CSharp;

    public class FSMGeneratorTest
    {
        #region Test Methods

        [Test]
        public void ConcreteStates()
        {
            FSMRepresentationBuilder fsmbld = InitBuilderState();
            StateMap map = fsmbld.StateMap;
            FSMGenerator fsm = new SMCSharpGenerator();
            fsm.FSMInit(map, "TurnStyle");
            fsm.Initialize();
            Assert.AreEqual(2, fsm.ConcreteStates.Count());
        }

        [Test]
        public void Paramaters()
        {
            FSMGenerator fsm = new SMCSharpGenerator();
            fsm.FSMInit(new MutableStateMap(), "TurnStyle");
            fsm.Initialize();
            Assert.AreEqual("TurnStyle", fsm.InputFileName);
            Assert.AreEqual("TurnStyle", fsm.FilePrefix);
        }

        #endregion

        #region Helper Methods

        private FSMRepresentationBuilder InitBuilderState()
        {
            var builder = new FSMRepresentationBuilder();

            builder.AddBuiltConcreteState(new ConcreteStateImpl("Locked"));
            builder.AddBuiltConcreteState(new ConcreteStateImpl("Unlocked"));

            builder.Build();
            return builder;
        }

        #endregion
    }
}
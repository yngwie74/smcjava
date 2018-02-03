namespace SMC.Builder.DataModel
{
    using System.Linq;

    using NUnit.Framework;

    using SMC.Builder;
    using SMC.FsmRep;

    [TestFixture]
    public class NormalStateRepTest
    {
        #region Constants

        private const string StateName = "StateName";

        #endregion

        #region Fields

        private StateRep stateRep;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            this.stateRep = new NormalStateRep(StateName, null);
        }

        [Test]
        public void NormalStateName()
        {
            State state = this.stateRep.Build(new FSMRepresentationBuilder());
            Assert.AreEqual(StateName, state.Name);
        }

        [Test]
        public void NormalStateEntryActions()
        {
            this.stateRep.AddEntryAction("EntryAction");
            this.stateRep.Build(new FSMRepresentationBuilder());
            Assert.AreEqual(1, this.stateRep.EntryActions.Count());
        }

        [Test]
        public void StringRepr()
        {
            Assert.AreEqual($"{StateName}", this.stateRep.ToString());
        }

        [Test]
        public void Equality()
        {
            StateRep other = new NormalStateRep(StateName, null);
            Assert.IsTrue(other.Equals(this.stateRep));
        }

        [Test]
        public void NotEquals()
        {
            StateRep other = new NormalStateRep("OtherStateName", null);
            Assert.IsFalse(other.Equals(this.stateRep));
        }

        #endregion
    }
}
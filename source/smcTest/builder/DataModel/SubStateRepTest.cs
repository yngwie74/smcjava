namespace SMC.Builder.DataModel
{
    using NUnit.Framework;

    using SMC.Builder;

    [TestFixture]
    public class SubStateRepTest
    {
        #region Constants

        private const string StateName = "SubStateName";
        private const string SuperStateName = "SuperStateName";

        #endregion

        #region Fields

        private StateRep stateRep;
        private FSMRepresentationBuilder builder;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            this.stateRep = new SubStateRep(StateName, SuperStateName, null);
            this.builder = new FSMRepresentationBuilder();
        }

        [Test]
        public void StateRepName()
        {
            Assert.AreEqual(StateName, this.stateRep.StateName);
        }

        [Test]
        public void StringRepr()
        {
            Assert.AreEqual($"{StateName}:{SuperStateName}", this.stateRep.ToString());
        }

        [Test]
        public void Equality()
        {
            StateRep other = new SubStateRep(StateName, SuperStateName, null);
            Assert.IsTrue(other.Equals(this.stateRep));
        }

        [Test]
        public void NotEquals()
        {
            StateRep other = new SubStateRep(StateName, "SuperStateNameIsDifferent", null);
            Assert.IsFalse(other.Equals(this.stateRep));
        }

        [Test]
        public void Errors()
        {
            Assert.IsNull(this.stateRep.Build(this.builder));
        }

        [Test]
        public void SuperStateNullErrors()
        {
            StateRep rep = new SubStateRep("SubState", null, null);
            Assert.IsNull(rep.Build(this.builder));
        }

        #endregion
    }
}
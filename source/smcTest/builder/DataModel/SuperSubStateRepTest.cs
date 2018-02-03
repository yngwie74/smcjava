namespace SMC.Builder.DataModel
{
    using NUnit.Framework;

    public class SuperSubStateRepTest
    {
        #region Constants

        private const string SuperStateName = "SuperState";
        private const string StateName = "SuperSubState";

        #endregion

        #region Fields

        private StateRep superSubState;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            this.superSubState = new SuperSubStateRep(StateName, SuperStateName, null);
        }

        [Test]
        public void StringRepr()
        {
            Assert.AreEqual($"({StateName}) : {SuperStateName}", this.superSubState.ToString());
        }

        [Test]
        public void Name()
        {
            Assert.AreEqual(StateName, this.superSubState.StateName);
        }

        [Test]
        public void Equality()
        {
            Assert.IsTrue(this.superSubState.Equals(new SuperSubStateRep(StateName, SuperStateName, null)));
        }

        [Test]
        public void NotEquals()
        {
            Assert.IsFalse(this.superSubState.Equals(new SuperSubStateRep("supersubState", SuperStateName, null)));
            Assert.IsFalse(this.superSubState.Equals(new SuperSubStateRep(StateName, "NotSameSuperState", null)));
        }

        #endregion
    }
}
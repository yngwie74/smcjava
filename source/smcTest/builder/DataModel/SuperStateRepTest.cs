namespace SMC.Builder.DataModel
{
    using NUnit.Framework;

    public class SuperStateRepTest
    {
        #region Constants

        private const string StateName = "SuperState";

        #endregion

        #region Fields

        private StateRep superState;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            this.superState = new SuperStateRep(StateName, null);
        }

        [Test]
        public void StringRepr()
        {
            Assert.AreEqual($"({StateName})", this.superState.ToString());
        }

        [Test]
        public void Name()
        {
            Assert.AreEqual(StateName, this.superState.StateName);
        }

        [Test]
        public void Equality()
        {
            StateRep other = new SuperStateRep(StateName, null);
            Assert.IsTrue(this.superState.Equals(other));
        }

        [Test]
        public void NotEquals()
        {
            StateRep other = new SuperStateRep("notSameSuperState", null);
            Assert.IsFalse(this.superState.Equals(other));
        }

        #endregion
    }
}
namespace SMC.Generator.CSharp
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class CSharpCodeGeneratorsTest
    {
        #region Public Methods

        [Test]
        public void TopLevelGenerators()
        {
            var instance = CSharpCodeGeneratorBuilder.Instance;
            Assert.AreEqual(4, instance.TopLevelGenerators().Count());
        }

        [Test]
        public void StateMachineGenerators()
        {
            var instance = CSharpCodeGeneratorBuilder.Instance;
            Assert.AreEqual(3, instance.StateMachineGenerators().Count());
        }

        [Test]
        public void StateGenerators()
        {
            var instance = CSharpCodeGeneratorBuilder.Instance;
            Assert.AreEqual(2, instance.StateGenerators().Count());
        }

        #endregion
    }
}
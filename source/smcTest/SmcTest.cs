namespace SMC
{
    using NUnit.Framework;

    [TestFixture]
    public class SmcTest
    {
        #region Constants

        private const string ExpectedFSMGeneratorName = "generatorName";
        private const string ExpectedInputFilename = "file";

        #endregion

        #region Fields

        private Smc smc;

        #endregion

        #region Public Methods

        [SetUp]
        public void SetUp()
        {
            this.smc = new Smc(ExpectedInputFilename);
        }

        [Test]
        public void Ctor_InputFilename_SetsPropertyValue()
        {
            Assert.AreEqual(ExpectedInputFilename, this.smc.InputFilename);
        }

        [Test]
        public void Ctor_InputFilenameNullOrEmpty_Throws([Values(null, "")]string fileName)
        {
            void act() => new Smc(fileName);
            Assert.That(act, Throws.ArgumentNullException);
        }

        [Test]
        public void Ctor_FSMGeneratorNameNotSet_SetsPropertyToEmpty()
        {
            Assert.AreEqual("", this.smc.FSMGeneratorName);
        }

        [Test]
        public void Ctor_FSMGeneratorNameNull_Throws()
        {
            void act() => new Smc(ExpectedInputFilename, null);
            Assert.That(act, Throws.ArgumentNullException);
        }

        [Test]
        public void FSMGeneratorName_SetValue_ReturnsValue(
            [Values("", ExpectedFSMGeneratorName)]string generatorName)
        {
            this.smc.FSMGeneratorName = generatorName;
            Assert.AreEqual(generatorName, this.smc.FSMGeneratorName);
        }

        [Test]
        public void FSMGeneratorName_SetNull_Throws()
        {
            void act() => this.smc.FSMGeneratorName = null;
            Assert.That(act, Throws.ArgumentNullException);
        }

        #endregion
    }
}
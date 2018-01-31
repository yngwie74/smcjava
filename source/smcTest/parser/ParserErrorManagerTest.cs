namespace SMC.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class ParserErrorManagerTest
    {
        #region Fields

        private ParserErrorManager manager;

        #endregion

        #region Public Methods

        [SetUp]
        public void SetUp()
        {
            this.manager = InitManager();
        }

        [Test]
        public void ErrorManager()
        {
            Assert.AreEqual(6, this.manager.Errors.Count());
        }

        [Test]
        public void Errors()
        {
            var errorList = this.manager.Errors.ToList();
            AssertList(errorList, 0);
            AssertList(errorList, 1);
            AssertList(errorList, 2);
            AssertList(errorList, 3);
            AssertList(errorList, 4);
            AssertList(errorList, 5);
        }

        #endregion

        #region Methods

        private static void AssertList(IList<string> errorList, int num)
        {
            var actual = errorList[num];
            Assert.AreEqual("Error" + num, actual);
        }

        private ParserErrorManager InitManager()
        {
            var man = new ParserErrorManager();
            man.Error("Error0");
            man.Error("Error1");
            man.Error("Error2");
            man.Error("Error3");
            man.Error("Error4");
            man.Error("Error5");
            return man;
        }

        #endregion
    }
}
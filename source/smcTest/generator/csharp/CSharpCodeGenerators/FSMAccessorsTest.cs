namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using static System.Environment;

    [TestFixture]
    public class FSMAccessorsTest : CSharpCodeGeneratorTest<FSMAccessors>
    {
        #region Constants

        private static readonly string VersionProperty = $"{Indent}public string Version => version;{NewLine}{NewLine}";

        private static readonly string CurrentStateNameProperty = $"{Indent}public string CurrentStateName => this.currentState.Name;{NewLine}{NewLine}";

        private static readonly string CurrentStateProperty = 
            $"{Indent}internal State CurrentState{NewLine}" +
            $"{Indent}{{{NewLine}" +
            $"{Indent}{Indent}get {{ return this.currentState; }}{NewLine}" +
            $"{Indent}{Indent}set {{ this.currentState = value; }}{NewLine}" +
            $"{Indent}}}{NewLine}{NewLine}";

        public static readonly string AccessorsRegion = 
            $"{Indent}#region Public Properties{NewLine}{NewLine}" +
            VersionProperty +
            CurrentStateNameProperty +
            CurrentStateProperty +
            $"{Indent}#endregion{NewLine}{NewLine}";

        private string generatedCode;

        #endregion

        #region Test Methods

        [SetUp]
        public void MySetUp()
        {
            var fsmbld = TestCSharpCodeGeneratorUtils.BuildDefaultTestConfig();
            this.generatedCode = GenerateUsing(fsmbld);
        }

        [Test]
        public void IncludesAccessorsRegion() => Assert.AreEqual(AccessorsRegion, this.generatedCode);

        [Test]
        public void IncludesVersion() => AssertResultContainsSubstring(VersionProperty);

        [Test]
        public void IncludesCurrentStateName() => AssertResultContainsSubstring(CurrentStateNameProperty);

        [Test]
        public void IncludesCurrentState() => AssertResultContainsSubstring(CurrentStateProperty);

        #endregion

        #region Helper Methods

        private void AssertResultContainsSubstring(string expected)
        {
            Assert.That(this.generatedCode, Contains.Substring(expected));
        }

        #endregion
    }
}
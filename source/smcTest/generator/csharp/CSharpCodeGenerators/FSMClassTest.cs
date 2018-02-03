namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using static System.Environment;

    [TestFixture]
    public class FSMClassTest
    {
        #region Constants

        private const string Indent = "    ";

        private static readonly string ClassHeader = 
            $"/// <summary>{NewLine}" +
            $"/// This is the Finite State Machine class{NewLine}" +
            $"/// <summary>{NewLine}";

        private static readonly string ClassDeclaration = 
            $"public class TurnStyle : TurnStyleContext{NewLine}" +
            $"{{{NewLine}";

        private static readonly string FieldsDeclaration = 
            $"{Indent}#region Fields{NewLine}{NewLine}" +
            $"{Indent}private static string version = \"\";{NewLine}{NewLine}" +
            $"{Indent}private State currentState;{NewLine}{NewLine}" +
            $"{Indent}#endregion{NewLine}{NewLine}";

        private static readonly string EmptyEventMethodsRegion = 
            $"{Indent}#region Event Methods - forward to the current State{NewLine}{NewLine}" +
            $"{Indent}#endregion{NewLine}{NewLine}";

        private static readonly string BuildFSMClass = 
            ClassHeader +
            ClassDeclaration +
            FieldsDeclaration +
            FSMConstructorTest.ConstructorRegion +
            FSMAccessorsTest.AccessorsRegion +
            EmptyEventMethodsRegion;

        private string generatedCode;

        #endregion

        #region Test Methods

        [SetUp]
        public void SetUp()
        {
            var fsmbld = TestCSharpCodeGeneratorUtils.InitBuilderState();

            var fsm = new SMCSharpGenerator();
            fsm.FSMInit(fsmbld.StateMap, "fileName");
            fsm.Initialize();

            this.generatedCode = new FSMClass().GenerateCode(fsm);
        }

        [Test]
        public void IncludesClassHeader() => AssertResultContainsSubstring(ClassHeader);

        [Test]
        public void IncludesClassDeclaration() => AssertResultContainsSubstring(ClassDeclaration);

        [Test]
        public void IncludesFieldsDeclaration() => AssertResultContainsSubstring(FieldsDeclaration);

        [Test]
        public void IncludesEmptyEventMethodsRegion() => AssertResultContainsSubstring(EmptyEventMethodsRegion);

        [Test]
        public void FSMClass() => Assert.That(this.generatedCode, Does.StartWith(BuildFSMClass));

        [Test]
        public void FromFile()
        {
            var actual = TestFileString.Instance.FileContents;
            Assert.That(actual, Contains.Substring("    public static State Locked { get; } = new LockedState();"));
            Assert.That(actual, Contains.Substring("    public static State Unlocked { get; } = new UnlockedState();"));
        }

        #endregion

        #region Helper Methods

        private void AssertResultContainsSubstring(string expected)
        {
            Assert.That(this.generatedCode, Contains.Substring(expected));
        }

        #endregion
    }
}
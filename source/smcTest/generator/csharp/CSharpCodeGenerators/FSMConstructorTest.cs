namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using static System.Environment;

    [TestFixture]
    public class FSMConstructorTest : CSharpCodeGeneratorTest<FSMConstructor>
    {
        #region Constants

        public static readonly string ConstructorDeclaration = 
            $"{Indent}public TurnStyle(){NewLine}" +
            $"{Indent}{{{NewLine}" +
            $"{Indent}{Indent}this.currentState = State.Locked;{NewLine}" +
            $"{Indent}}}{NewLine}{NewLine}";

        public static readonly string ConstructorRegion = 
            $"{Indent}#region Constructors & Destructors{NewLine}{NewLine}" +
            ConstructorDeclaration +
            $"{Indent}#endregion{NewLine}{NewLine}";

        #endregion

        #region Fields

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
        public void IncludesConstructorDeclaration()
            => Assert.That(this.generatedCode, Contains.Substring(ConstructorDeclaration));

        [Test]
        public void FullConstructorRegion()
            => Assert.AreEqual(ConstructorRegion, this.generatedCode);

        #endregion
    }
}
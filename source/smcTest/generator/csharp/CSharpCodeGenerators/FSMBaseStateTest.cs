namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using static System.Environment;

    using NUnit.Framework;

    [TestFixture]
    public class FSMBaseStateTest : CSharpCodeGeneratorTest<FSMBaseState>
    {
        #region Constants

        public static readonly string ClassHeader = $"/// <summary>{NewLine}" +
            $"/// This is the base State class{NewLine}" +
            $"/// </summary>{NewLine}";

        public static readonly string ClassDeclarationOpening = $"internal abstract class State{NewLine}" +
            $"{{{NewLine}";

        public static readonly string StatePropertiesRegion = $"{Indent}#region Static Properties For Each State{NewLine}{NewLine}" +
            $"{Indent}public static State Locked {{ get; }} = new LockedState();{NewLine}" +
            $"{Indent}public static State Unlocked {{ get; }} = new UnlockedState();{NewLine}{NewLine}" +
            $"{Indent}#endregion{NewLine}{NewLine}";

        public static readonly string PublicPropertiesRegion = $"{Indent}#region Public Properties{NewLine}{NewLine}" +
            $"{Indent}public abstract string Name {{ get; }}{NewLine}{NewLine}" +
            $"{Indent}#endregion{NewLine}{NewLine}";

        public static readonly string EmptyEventMethodsRegion = $"{Indent}#region Default Event Methods{NewLine}{NewLine}" +
            $"{Indent}#endregion{NewLine}";

        public static readonly string ClassDeclarationClosing = $"}}{NewLine}{NewLine}";

        public static readonly string BuildFSMBaseState =
            ClassHeader +
            ClassDeclarationOpening +
            StatePropertiesRegion +
            PublicPropertiesRegion +
            EmptyEventMethodsRegion +
            ClassDeclarationClosing;

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
        public void IncludesClassHeader()
            => Assert.That(this.generatedCode, Does.StartWith(ClassHeader));

        [Test]
        public void IncludesStatePropertiesRegion()
            => Assert.That(this.generatedCode, Contains.Substring(StatePropertiesRegion));

        [Test]
        public void IncludesPublicPropertiesRegion()
            => Assert.That(this.generatedCode, Contains.Substring(PublicPropertiesRegion));

        [Test]
        public void IncludesEventMethodsRegion()
            => Assert.That(this.generatedCode, Contains.Substring(EmptyEventMethodsRegion));

        [Test]
        public void IncludesClassDeclarationClosing()
            => Assert.That(this.generatedCode, Does.EndWith(ClassDeclarationClosing));

        [Test]
        public void FSMBaseState()
        {
            var expected = BuildFSMBaseState;
            Assert.AreEqual(expected, this.generatedCode);
        }

        #endregion
    }
}
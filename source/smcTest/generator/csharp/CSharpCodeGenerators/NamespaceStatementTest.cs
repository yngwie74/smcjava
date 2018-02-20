namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using SMC.Builder;

    using static System.Environment;

    [TestFixture]
    public class NamespaceStatementTest : CSharpCodeGeneratorTest<NamespaceStatement>
    {
        [Test]
        public void NoNamespacePragma()
        {
            var fsmbld = new FSMRepresentationBuilder();

            var actual = GenerateUsing(fsmbld);

            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void NamespaceStatements()
        {
            var fsmbld = TestCSharpCodeGeneratorUtils.BuildDefaultTestConfig();

            var actual = GenerateUsing(fsmbld);

            var expected = $"namespace TurnStyleExample{NewLine}{{{NewLine}";
            Assert.AreEqual(expected, actual);
        }
    }
}
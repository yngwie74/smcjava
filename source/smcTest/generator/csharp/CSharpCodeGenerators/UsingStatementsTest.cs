namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using SMC.Builder;

    using static System.Environment;

    public class UsingStatementsTest : CSharpCodeGeneratorTest<UsingStatements>
    {
        [Test]
        public void NoUsingPragmas()
        {
            var fsmbld = new FSMRepresentationBuilder();

            var actual = GenerateUsing(fsmbld);

            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void ManyUsingStatements()
        {
            var fsmbld = TestCSharpCodeGeneratorUtils.InitBuildStateWithTwoUsings();

            var actual = GenerateUsing(fsmbld);

            var expected = $"using aClass;{NewLine}using bClass;{NewLine}{NewLine}";
            Assert.AreEqual(expected, actual);
        }
    }
}
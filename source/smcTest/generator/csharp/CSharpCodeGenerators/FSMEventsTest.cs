namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using static System.Environment;

    [TestFixture]
    public class FSMEventsTest : CSharpCodeGeneratorTest<FSMEvents>
    {
        #region Fields

        public static readonly string BuildFSMEvents =
            $"{Indent}#region Event Methods - forward to the current State{NewLine}"+
            $"{NewLine}" +
            $"{Indent}#endregion{NewLine}{NewLine}";

        #endregion

        #region Test Methods

        [Test]
        public void Events()
        {
            var fsmbld = TestCSharpCodeGeneratorUtils.InitBuilderState();
            var actual = GenerateUsing(fsmbld);
            Assert.AreEqual(BuildFSMEvents, actual);
        }

        #endregion
    }
}
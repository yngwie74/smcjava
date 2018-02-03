namespace SMC.Generator.CSharp
{
    using NUnit.Framework;

    [TestFixture]
    public class SMCSharpGeneratorTest
    {
        [Test]
        public void Creation()
        {
            var gen = new SMCSharpGenerator();
            Assert.IsNotNull(gen);
        }
    }
}
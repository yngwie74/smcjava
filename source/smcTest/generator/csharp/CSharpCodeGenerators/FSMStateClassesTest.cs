namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using static System.Environment;

    using NUnit.Framework;

    [TestFixture]
    public class FSMStateClassesTest : CSharpCodeGeneratorTest<FSMStateClasses>
    {
        #region Constants

        private static readonly string LockedState =
            $"/// <summary>{NewLine}" +
            $"/// This class handles the \"Locked\" state and its events{NewLine}" +
            $"/// </summary>{NewLine}" +
            $"internal class LockedState : State{NewLine}" +
            $"{{{NewLine}" +
            $"    public override string Name => \"Locked\";{NewLine}{NewLine}" +
            $"    public override void Coin(TurnStyle name){NewLine}" +
            $"    {{{NewLine}" +
            $"        name.Unlock();{NewLine}{NewLine}" +
            $"        // change the state{NewLine}" +
            $"        name.CurrentState = State.Unlocked;{NewLine}" +
            $"    }}{NewLine}{NewLine}" +
            $"    public override void Pass(TurnStyle name){NewLine}" +
            $"    {{{NewLine}" +
            $"        name.Alarm();{NewLine}{NewLine}" +
            $"        // change the state{NewLine}" +
            $"        name.CurrentState = State.Locked;{NewLine}" +
            $"    }}{NewLine}" +
            $"}}{NewLine}";

        private static readonly string UnlockedState =
            $"/// <summary>{NewLine}" +
            $"/// This class handles the \"Unlocked\" state and its events{NewLine}" +
            $"/// </summary>{NewLine}" +
            $"internal class UnlockedState : State{NewLine}" +
            $"{{{NewLine}" +
            $"    public override string Name => \"Unlocked\";{NewLine}{NewLine}" +
            $"    public override void Coin(TurnStyle name){NewLine}" +
            $"    {{{NewLine}" +
            $"        name.Thankyou();{NewLine}{NewLine}" +
            $"        // change the state{NewLine}" +
            $"        name.CurrentState = State.Unlocked;{NewLine}" +
            $"    }}{NewLine}{NewLine}" +
            $"    public override void Pass(TurnStyle name){NewLine}" +
            $"    {{{NewLine}" +
            $"        name.Lock();{NewLine}{NewLine}" +
            $"        // change the state{NewLine}" +
            $"        name.CurrentState = State.Locked;{NewLine}" +
            $"    }}{NewLine}" +
            $"}}{NewLine}";

        #endregion

        #region Test Methods

        [Test]
        public void IncludesLockedStateClass()
        {
            var generatedCode = TestFileString.Instance.FileContents;
            Assert.That(generatedCode, Contains.Substring(LockedState));
        }

        [Test]
        public void IncludesUnlockedStateClass()
        {
            var generatedCode = TestFileString.Instance.FileContents;
            Assert.That(generatedCode, Contains.Substring(UnlockedState));
        }

        #endregion
    }
}
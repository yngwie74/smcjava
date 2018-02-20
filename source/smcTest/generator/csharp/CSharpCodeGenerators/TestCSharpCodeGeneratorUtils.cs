namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using SMC.Builder;
    using SMC.FsmRep;

    public class TestCSharpCodeGeneratorUtils
    {
        public static FSMRepresentationBuilder InitBuildStateWithTwoUsings()
        {
            var builder = new FSMRepresentationBuilder();
            builder.AddPragma("using aClass");
            builder.AddPragma("using bClass");

            return builder;
        }

        public static FSMRepresentationBuilder BuildDefaultTestConfig(
            bool usesExceptions = true, string version = "")
        {
            var builder = GetDefaultTestConfig(usesExceptions, version);
            builder.Build();
            return builder;
        }

        public static FSMRepresentationBuilder GetDefaultTestConfig(
            bool usesExceptions = true, string version = "")
        {
            var builder = new FSMRepresentationBuilder();
            builder.AddPragma("namespace TurnStyleExample");
            builder.SetName("TurnStyle");
            builder.SetContextName("TurnStyleContext");
            builder.SetVersion(version);

            if (usesExceptions)
            {
                builder.SetException("Exception");
            }

            builder.AddBuiltConcreteState(new ConcreteStateImpl("Locked"));
            builder.AddBuiltConcreteState(new ConcreteStateImpl("Unlocked"));

            builder.SetInitialState("Locked");

            builder.AddTransition("Coin", "Unlocked", null);
            builder.AddTransition("Pass", "Locked", null);

            builder.AddAction("Unlock");
            builder.AddAction("Lock");
            builder.AddAction("Alarm");
            builder.AddAction("Thankyou");

            return builder;
        }
    }
}
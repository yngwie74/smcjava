namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using smc.fsmrep;
    using smc.generator.csharp;

    public class FSMConstructor : CSharpCodeGenerator
    {
        public override string generateCode(SMCSharpGenerator gen)
        {
            var buff = new StringBuilder()
                .AppendLine("    #region Constructors & Destructors")
                .AppendLine()
                .AppendLine($"    public {gen.getStateMap().getName()}()")
                .AppendLine("    {");

            var iName = createMethodName(gen.getStateMap().getInitialState());

            buff.Append($"        currentState = State.{iName};");

            var initialHierarchy = new List<State>();
            gen.getStateHierarchy(initialHierarchy, gen.getStateMap().getInitialState());

            foreach (var newState in initialHierarchy)
            {
                var eactions = newState.getEntryActions();
                if (eactions.Any())
                {
                    buff.AppendLine()
                        .AppendLine($"        // Entry functions for: {newState.getName()}");
                }

                foreach (var action in eactions)
                {
                    buff.AppendLine($"        {action}();");
                }
            }

            buff.AppendLine("    }")
                .AppendLine()
                .AppendLine("    #endregion")
                .AppendLine();

            return buff.ToString();
        }
    }
}

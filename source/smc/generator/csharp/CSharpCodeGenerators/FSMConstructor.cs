namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SMC.FsmRep;
    using SMC.Generator.CSharp;

    public class FSMConstructor : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator gen)
        {
            var buff = new StringBuilder()
                .AppendLine("    #region Constructors & Destructors")
                .AppendLine()
                .AppendLine($"    public {gen.StateMap.Name}()")
                .AppendLine("    {");

            var iName = CreateMethodName(gen.StateMap.InitialState);

            buff.AppendLine($"        this.currentState = State.{iName};");

            var initialHierarchy = new List<State>();
            gen.GetStateHierarchy(initialHierarchy, gen.StateMap.InitialState);

            foreach (var newState in initialHierarchy)
            {
                var eactions = newState.EntryActions;
                if (eactions.Any())
                {
                    buff.AppendLine()
                        .AppendLine($"        // Entry functions for: {newState.Name}");
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

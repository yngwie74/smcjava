namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System;
    using System.Linq;
    using System.Text;

    using SMC.FsmRep;
    using SMC.Generator.CSharp;

    public class FSMClass : CSharpCodeGenerator
    {
        #region Public Methods

        public override string GenerateCode(SMCSharpGenerator gen)
        {
            var stateMap = gen.StateMap;
            var buff = new StringBuilder();

            AddClassHeader(buff);
            BeginClassDeclaration(buff, stateMap);
            AddFields(buff, stateMap);
            AddClassMembers(gen, buff);

            AddStateClasses(gen, buff);

            CloseClassDeclaration(buff);

            return buff.ToString();
        }

        #endregion

        #region Methods

        private static void AddClassHeader(StringBuilder buff)
        {
            buff.AppendLine("/// <summary>")
                .AppendLine("/// This is the Finite State Machine class")
                .AppendLine("/// <summary>");
        }

        private static void BeginClassDeclaration(StringBuilder buff, StateMap stateMap)
        {
            var className = stateMap.Name;
            var superClassName = stateMap.ContextName;
            var qualifier = IsAbstract(stateMap) ? "abstract " : string.Empty;

            buff.AppendLine($"public {qualifier}class {className} : {superClassName}")
                    .AppendLine("{");
        }

        private static bool IsAbstract(StateMap stateMap) => stateMap.Pragma.Any(p => Matches(p.Trim(), "abstract"));

        private static bool Matches(string a, string b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;

        private static void AddFields(StringBuilder buff, StateMap stateMap)
        {
            buff.AppendLine("    #region Fields")
                .AppendLine()
                .AppendLine($"    private static string version = \"{stateMap.Version}\";")
                .AppendLine()
                .AppendLine("    private State currentState;")
                .AppendLine()
                .AppendLine("    #endregion")
                .AppendLine();
        }

        private static void AddClassMembers(SMCSharpGenerator gen, StringBuilder buff) => AddFromGenerators(gen, CSharpCodeGeneratorBuilder.Instance.StateMachineGenerators, buff);

        private static void CloseClassDeclaration(StringBuilder buff) => buff.AppendLine("}");

        private static void AddStateClasses(SMCSharpGenerator gen, StringBuilder buff) => AddFromGenerators(gen, CSharpCodeGeneratorBuilder.Instance.StateGenerators, buff);

        #endregion
    }
}
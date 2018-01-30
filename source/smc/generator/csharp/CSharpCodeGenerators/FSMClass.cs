namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Text;

    using smc.generator.csharp;

    public class FSMClass : CSharpCodeGenerator
    {
        #region Public Methods

        public override string generateCode(SMCSharpGenerator gen)
        {
            var stateMap = gen.getStateMap();
            var buff = new StringBuilder();

            AddClassHeader(buff);
            BeginClassDeclaration(buff, stateMap);
            AddFields(buff, stateMap);
            AddClassMembers(gen, buff);
            CloseClassDeclaration(buff);

            AddStateClasses(gen, buff);

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

        private static void BeginClassDeclaration(StringBuilder buff, fsmrep.StateMap stateMap)
        {
            var className = stateMap.getName();
            var superClassName = stateMap.getContextName();

            buff.AppendLine($"public class {className} : {superClassName}")
                .AppendLine("{");
        }

        private static void AddFields(StringBuilder buff, fsmrep.StateMap stateMap)
        {
            buff.AppendLine($"    private static string version = \"{stateMap.getVersion()}\";")
                .AppendLine()
                .AppendLine("    private State currentState;")
                .AppendLine();
        }

        private static void AddClassMembers(SMCSharpGenerator gen, StringBuilder buff)
        {
            try
            {
                AddFromGenerators(gen, buff, CSharpCodeGeneratorBuilder.Instance.StateMachineGenerators);
            }
            catch (System.Exception)
            { }
        }

        private static void CloseClassDeclaration(StringBuilder buff)
        {
            buff.AppendLine("}")
                .AppendLine();
        }

        private static void AddStateClasses(SMCSharpGenerator gen, StringBuilder buff)
        {
            try
            {
                AddFromGenerators(gen, buff, CSharpCodeGeneratorBuilder.Instance.StateGenerators);
            }
            catch (System.Exception)
            { }
        }

        #endregion
    }
}
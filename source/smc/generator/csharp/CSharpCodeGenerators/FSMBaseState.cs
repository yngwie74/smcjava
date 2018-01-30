namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;

    using SMC.Generator.CSharp;

    public class FSMBaseState : CSharpCodeGenerator
    {
        #region Constants

        private const string ArgName = "name";

        #endregion

        #region Public Methods

        public override string GenerateCode(SMCSharpGenerator gen)
        {
            var buff = new StringBuilder();

            AddClassHeader(buff);
            AddOpeningClassDeclaration(buff);

            AddConcreteStates(gen, buff);
            AddPublicProperties(buff);
            AddEventMethods(gen, buff);
            AddNestedStateClasses(gen, buff);

            AddClosingClassDeclaration(buff);

            return buff.ToString();
        }

        #endregion

        #region Methods

        private static void AddClassHeader(StringBuilder buff) => buff
            .AppendLine("/// <summary>")
            .AppendLine("/// This is the base State class")
            .AppendLine("/// </summary>");

        private static void AddOpeningClassDeclaration(StringBuilder buff) => buff
            .AppendLine("public abstract class State")
            .AppendLine("{");

        private void AddConcreteStates(SMCSharpGenerator gen, StringBuilder buff)
        {
            foreach (var cs in gen.ConcreteStates)
            {
                buff.Append($"    public static State {CreateMethodName(cs)} ")
                    .AppendLine($"{{ get; }} = new {ClassNameFor(cs)}();");
            }

            buff.AppendLine();
        }

        private static void AddPublicProperties(StringBuilder buff) => buff
            .AppendLine("    public abstract string Name { get; }")
            .AppendLine();

        private void AddEventMethods(SMCSharpGenerator gen, StringBuilder buff)
        {
            var events = gen.StateMap.Events;
            foreach (var evName in events)
            {
                AddEventHeader(evName, buff);
                AddOpenEventDeclaration(gen, evName, buff);
                AddEventBody(gen, evName, buff);
                AddCloseEventDeclaration(buff);
            }
        }

        private static void AddEventHeader(string evName, StringBuilder buff) => buff
            .AppendLine("    /// <summary>")
            .AppendLine($"    /// Responds to {evName} event")
            .AppendLine("    /// </summary>");

        private void AddOpenEventDeclaration(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            var stateMachineClass = gen.StateMap.Name;
            var methodName = this.CreateMethodName(evName);
            buff.AppendLine($"    public void {methodName}({stateMachineClass} {ArgName})")
                .AppendLine("    {");
        }

        private static void AddEventBody(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            if (gen.StateMap.UsesExceptions)
            {
                AddThrowException(gen, evName, buff);
            }
            else
            {
                AddErrorFuncCall(gen, evName, buff);
            }
        }

        private static void AddThrowException(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            var exceptionName = gen.StateMap.ExceptionName;
            buff.Append($"        throw new {exceptionName}")
                .AppendLine($"(\"{evName}\", {ArgName}.CurrentStateName);");
        }

        private static void AddErrorFuncCall(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            var errorFunctionName = gen.StateMap.ErrorFunctionName;
            buff.Append($"        {ArgName}.{errorFunctionName}")
                .AppendLine($"(\"{evName}\", {ArgName}.CurrentState);");
        }

        private static void AddCloseEventDeclaration(StringBuilder buff)
            => buff.AppendLine("    }").AppendLine();

        private static void AddNestedStateClasses(SMCSharpGenerator gen, StringBuilder buff)
            => buff.Append(new FSMStateClasses().GenerateCode(gen));

        private static void AddClosingClassDeclaration(StringBuilder buff)
            => buff.AppendLine("}");

        #endregion
    }
}
namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Text;

    using smc.generator.csharp;

    public class FSMBaseState : CSharpCodeGenerator
    {
        #region Constants

        private const string ArgName = "name";

        #endregion

        #region Public Methods

        public override string generateCode(SMCSharpGenerator gen)
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
            foreach (var cs in gen.getConcreteStates())
            {
                buff.Append($"    public static State {createMethodName(cs)} ")
                    .AppendLine($"{{ get; }} = new {classNameFor(cs)}();");
            }

            buff.AppendLine();
        }

        private static void AddPublicProperties(StringBuilder buff) => buff
            .AppendLine("    public abstract string Name { get; }")
            .AppendLine();

        private void AddEventMethods(SMCSharpGenerator gen, StringBuilder buff)
        {
            var events = gen.getStateMap().getEvents();
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
            var stateMachineClass = gen.getStateMap().getName();
            var methodName = this.createMethodName(evName);
            buff.AppendLine($"    public virtual void {methodName}({stateMachineClass} {ArgName})")
                .AppendLine("    {");
        }

        private static void AddEventBody(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            if (gen.usesExceptions(gen.getStateMap()))
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
            var exceptionName = gen.getStateMap().getExceptionName();
            buff.Append($"        throw new {exceptionName}")
                .AppendLine($"(\"{evName}\", {ArgName}.CurrentStateName);");
        }

        private static void AddErrorFuncCall(SMCSharpGenerator gen, string evName, StringBuilder buff)
        {
            var errorFunctionName = gen.getStateMap().getErrorFunctionName();
            buff.Append($"        {ArgName}.{errorFunctionName}")
                .AppendLine($"(\"{evName}\", {ArgName}.CurrentState);");
        }

        private static void AddCloseEventDeclaration(StringBuilder buff)
            => buff.AppendLine("    }").AppendLine();

        private static void AddNestedStateClasses(SMCSharpGenerator gen, StringBuilder buff)
            => buff.Append(new FSMStateClasses().generateCode(gen));

        private static void AddClosingClassDeclaration(StringBuilder buff)
            => buff.AppendLine("}");

        #endregion
    }
}
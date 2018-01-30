namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Text;

    using smc.generator.csharp;

    public class FSMAccessors : CSharpCodeGenerator
    {
        public override string generateCode(SMCSharpGenerator smcsg)
        {
            return new StringBuilder()
                .AppendLine()
                .AppendLine("    public string Version => version;")
                .AppendLine()
                .AppendLine("    public string CurrentStateName => this.currentState.Name;")
                .AppendLine()
                .AppendLine("    public State CurrentState")
                .AppendLine("    {")
                .AppendLine("        get { return this.currentState; }")
                .AppendLine("        set { this.currentState = value; }")
                .AppendLine("    }")
                .AppendLine()
                .ToString();
        }
    }
}
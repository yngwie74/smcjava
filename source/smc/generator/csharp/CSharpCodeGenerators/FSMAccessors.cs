namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;

    using SMC.Generator.CSharp;

    public class FSMAccessors : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator smcsg)
        {
            return new StringBuilder()
                .AppendLine("    #region Public Properties")
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
                .AppendLine("    #endregion")
                .AppendLine()
                .ToString();
        }
    }
}
namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;

    using SMC.Generator.CSharp;

    public class FSMEvents : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator gen)
        {
            var buff = new StringBuilder()
                .AppendLine("    #region Event Methods - forward to the current State")
                .AppendLine();

            var events = gen.StateMap.Events;
            foreach (var evi in events)
            {
                var evName = CreateMethodName(evi);
                buff.Append($"    public void {evName}() ")
                    .AppendLine($"=> this.currentState.{evName}(this);")
                    .AppendLine();
            }

            buff.AppendLine("    #endregion");

            return buff.ToString();
        }
    }
}

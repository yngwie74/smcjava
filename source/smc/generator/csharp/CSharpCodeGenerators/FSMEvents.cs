namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Text;

    using smc.generator.csharp;

    public class FSMEvents : CSharpCodeGenerator
    {
        public override string generateCode(SMCSharpGenerator smcsg)
        {
            var buff = new StringBuilder()
                .AppendLine("    #region Event Methods - forward to the current State")
                .AppendLine();

            var events = smcsg.getStateMap().getEvents();
            foreach (var evi in events)
            {
                var evName = createMethodName(evi);
                buff.Append($"    public void {evName}() ")
                    .AppendLine($"=> this.currentState.{evName}(this);")
                    .AppendLine();
            }

            buff.AppendLine("    #endregion");

            return buff.ToString();
        }
    }
}

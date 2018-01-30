namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Text;

    using smc.generator.csharp;

    public class UsingStatements : CSharpCodeGenerator
    {
        public override string generateCode(SMCSharpGenerator smcsg)
        {
            var buff = new StringBuilder();
            if (smcsg.hasUsing())
            {
                foreach(var item in smcsg.getItsUsing())
                {
                    buff.AppendFormat("using {0};\n", item);
                }

                return buff.ToString();
            }

            return string.Empty;
        }
    }
}
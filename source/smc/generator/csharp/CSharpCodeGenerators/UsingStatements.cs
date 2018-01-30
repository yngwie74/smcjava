namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;

    using SMC.Generator.CSharp;

    public class UsingStatements : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator smcsg)
        {
            var buff = new StringBuilder();
            if (smcsg.HasUsing)
            {
                foreach(var item in smcsg.Usings)
                {
                    buff.AppendFormat("using {0};\n", item);
                }

                return buff.ToString();
            }

            return string.Empty;
        }
    }
}
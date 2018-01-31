namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;

    using SMC.Generator.CSharp;

    public class UsingStatements : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator gen)
            => GenerateCode(gen, new StringBuilder());

        public string GenerateCode(SMCSharpGenerator gen, StringBuilder buff)
        {
            if (gen.HasUsing)
            {
                foreach (var item in gen.Usings)
                {
                    buff.AppendLine($"using {item};");
                }

                buff.AppendLine();
            }

            return buff.ToString();
        }
    }
}
namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Text;
    using SMC.Generator.CSharp;

    public class NamespaceStatement : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator gen)
            => this.GenerateCode(gen, new StringBuilder());

        public string GenerateCode(SMCSharpGenerator gen, StringBuilder buff)
        {
            if (gen.HasNamespace)
            {
                buff.AppendLine($"namespace {gen.Namespace}")
                    .AppendLine("{");
            }

            return buff.ToString();
        }
    }
}
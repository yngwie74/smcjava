namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using SMC.Generator.CSharp;

    public class NamespaceStatement : CSharpCodeGenerator
    {
        public override string GenerateCode(SMCSharpGenerator gen)
        {
            return gen.HasNamespace
                ? $"namespace {gen.Namespace}\n{{\n"
                : string.Empty;
        }
    }
}
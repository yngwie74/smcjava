namespace smc.generator.csharp.CSharpCodeGenerators
{
    using smc.generator.csharp;

    public class NamespaceStatement : CSharpCodeGenerator
    {
        public override string generateCode(SMCSharpGenerator smcsg)
        {
            return smcsg.hasNamespace()
                ? "namespace {smcsg.getNamespace()}\n{\n"
                : string.Empty;
        }
    }
}
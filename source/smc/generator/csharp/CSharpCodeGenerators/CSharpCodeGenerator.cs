namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SMC.FsmRep;
    using SMC.Generator.CSharp;

    public abstract class CSharpCodeGenerator
    {
        public abstract string GenerateCode(SMCSharpGenerator gen);

        public string PrintSeparator(int level) => string.Empty;

        public string ClassNameFor(State s) => $"{CreateMethodName(s)}State";

        public string CreateMethodName(State s) => CreateMethodName(s.Name);

        public string CreateMethodName(string _event)
        {
            var length = _event.Length;
            var buff = new StringBuilder(length);
            if (length > 0)
            {
                buff.Append(char.ToUpperInvariant(_event[0]));
                if (length > 1)
                {
                    buff.Append(_event.Substring(1));
                }
            }

            return buff.ToString();
        }

        public static StringBuilder AddFromGenerators(SMCSharpGenerator gen, Func<IEnumerable<CSharpCodeGenerator>> generatorFactory)
            => AddFromGenerators(gen, generatorFactory, new StringBuilder());

        public static StringBuilder AddFromGenerators(SMCSharpGenerator gen, Func<IEnumerable<CSharpCodeGenerator>> generatorFactory, StringBuilder buff)
        {
            var generators = generatorFactory.Invoke();
            foreach (var code in generators)
            {
                buff.Append(code.GenerateCode(gen));
            }
            return buff;
        }
    }
}
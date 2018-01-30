namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using smc.fsmrep;
    using smc.generator.csharp;

    public abstract class CSharpCodeGenerator
    {
        public abstract string generateCode(SMCSharpGenerator gen);

        public virtual string printSeparator(int i)
        {
            return string.Empty;
        }

        public string classNameFor(State s)
        {
            return $"{createMethodName(s)}State";
        }

        public string createMethodName(State s)
        {
            return createMethodName(s.getName());
        }

        public string createMethodName(string @event)
        {
            var length = @event.Length;
            var buff = new StringBuilder(length);
            if (length > 0)
            {
                buff.Append(char.ToLowerInvariant(@event[0]));
                if (length > 1)
                {
                    buff.Append(@event.Substring(1));
                }
            }

            return buff.ToString();
        }

        public static StringBuilder AddFromGenerators(SMCSharpGenerator gen, StringBuilder buff, Func<IEnumerable<CSharpCodeGenerator>> generatorFactory)
        {
            var generators = generatorFactory.Invoke();
            foreach (var code in generators)
            {
                buff.Append(code.generateCode(gen));
            }
            return buff;
        }
    }
}

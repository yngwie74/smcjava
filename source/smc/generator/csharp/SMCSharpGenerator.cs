namespace smc.generator.csharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using smc.fsmrep;
    using smc.generator.csharp.CSharpCodeGenerators;

    public class SMCSharpGenerator : FSMGenerator
    {
        #region Fields

        private bool itHasNamespace;
        private bool itHasUsing;
        private string itsNamespace;
        private List<string> itsUsing;
        private ConcreteState itsSourceState;
        private ISet<string> itsOverriddenEvents;

        #endregion

        #region Constructors & Destructors

        public SMCSharpGenerator()
        {
            itsOverriddenEvents = new HashSet<string>();
            itHasNamespace = itHasUsing = false;
            itsUsing = new List<string>();
        }

        #endregion

        #region Public Methods

        public override void initialize()
        {
            initNamespaceAndUsingStatementes();
        }

        public bool usesExceptions(StateMap sm)
        {
            return (sm.getExceptionName().Length > 0);
        }

        public override void generate()
        {
            try
            {
                File.WriteAllText(
                    path: createOutputFileName(),
                    contents: generateStringForCode(),
                    encoding: Encoding.UTF8);
            }
            catch (IOException)
            {
                Console.WriteLine("Error: could not create output file");
                throw;
            }
        }

        public string generateStringForCode()
        {
            var buff = new StringBuilder();

            try
            {
                var builder = CSharpCodeGeneratorBuilder.Instance;
                CSharpCodeGenerator.AddFromGenerators(this, buff, builder.TopLevelGenerators)
                    .AppendLine("}")
                    .AppendLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return buff.ToString();
        }

        public bool hasNamespace()
        {
            return itHasNamespace;
        }

        public bool hasUsing()
        {
            return itHasUsing;
        }

        public override IEnumerable<string> getGeneratedFileNames()
        {
            return new string[] { createOutputFileName() };
        }

        public ConcreteState getItsSourceState()
        {
            return itsSourceState;
        }

        public void clearItsOverRiddenEvents()
        {
            itsOverriddenEvents.Clear();
        }

        public void setItsSourceState(ConcreteState value)
        {
            itsSourceState = value;
        }

        public ISet<string> getItsOverriddenEvents()
        {
            return itsOverriddenEvents;
        }

        public string getNamespace()
        {
            return itsNamespace;
        }

        public List<string> getItsUsing()
        {
            return itsUsing;
        }

        #endregion

        #region Methods

        private void initNamespaceAndUsingStatementes()
        {
            const string usingString = "Using";
            const string nspString = "Namespace";

            var useLen = usingString.Length;
            var nspLen = nspString.Length;

            var pragmas = getStateMap().getPragma();
            foreach (var p in pragmas)
            {
                try
                {
                    if (p.StartsWith(usingString, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = p.Substring(useLen);
                        itsUsing.Add(value.Trim());
                        itHasUsing = true;
                    }
                    else if (p.StartsWith(nspString, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = p.Substring(nspLen);
                        itsNamespace = value.Trim();
                        itHasNamespace = true;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Unknown pragma: {p}");
                    }
                }
                catch (Exception)
                { }
            }
        }

        private string createOutputFileName()
        {
            return $"{getDirectory()}{getStateMap().getName()}.cs";
        }

        #endregion
    }
}

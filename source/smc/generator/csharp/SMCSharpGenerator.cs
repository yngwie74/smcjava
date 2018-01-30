namespace SMC.Generator.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using SMC.FsmRep;
    using SMC.Generator.CSharp.CSharpCodeGenerators;

    public class SMCSharpGenerator : FSMGenerator
    {
        #region Fields

        private IList<string> itsUsing;
        private ISet<string> overriddenEvents;

        #endregion

        #region Constructors & Destructors

        public SMCSharpGenerator()
        {
            this.overriddenEvents = new HashSet<string>();
            this.itsUsing = new List<string>();
        }

        #endregion

        #region Public Properties

        public bool HasNamespace => !string.IsNullOrEmpty(this.Namespace);

        public bool HasUsing => this.itsUsing.Count > 0;

        public override IEnumerable<string> GeneratedFileNames
            => Enumerable.Repeat(CreateOutputFileName(), 1);

        public ConcreteState SourceState { get; set; }

        public string Namespace { get; private set; }

        public IEnumerable<string> Usings => this.itsUsing;

        #endregion

        #region Public Methods

        public override void Initialize() => InitNamespaceAndUsingStatementes();

        public override void Generate()
        {
            try
            {
                File.WriteAllText(
                    path: CreateOutputFileName(),
                    contents: GenerateStringForCode(),
                    encoding: Encoding.UTF8);
            }
            catch (IOException)
            {
                Console.WriteLine("Error: could not create output file");
                throw;
            }
        }

        public string GenerateStringForCode()
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

        public void ClearOverRiddenEvents() => this.overriddenEvents.Clear();

        public void AddOverRiddenEvent(string theEvent) => this.overriddenEvents.Add(theEvent);

        public bool IsOverRiddenEvent(string theEvent) => this.overriddenEvents.Contains(theEvent);

        #endregion

        #region Methods

        private void InitNamespaceAndUsingStatementes()
        {
            const string usingString = "Using";
            const string nspString = "Namespace";

            var useLen = usingString.Length;
            var nspLen = nspString.Length;

            var pragmas = this.StateMap.Pragma;
            foreach (var p in pragmas)
            {
                try
                {
                    if (p.StartsWith(usingString, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = p.Substring(useLen);
                        this.itsUsing.Add(value.Trim());
                    }
                    else if (p.StartsWith(nspString, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = p.Substring(nspLen);
                        this.Namespace = value.Trim();
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

        private string CreateOutputFileName() => $"{this.Directory}{this.StateMap.Name}.cs";

        #endregion
    }
}
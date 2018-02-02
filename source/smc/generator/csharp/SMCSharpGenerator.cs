namespace SMC.Generator.CSharp
{
    using System;
    using System.Collections.Generic;

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

        public ConcreteState SourceState { get; set; }

        public string Namespace { get; private set; }

        public IEnumerable<string> Usings => this.itsUsing;

        public string OutputFileName => $"{this.StateMap.Name}.cs";

        #endregion

        #region Public Methods

        public override void Initialize() => InitNamespaceAndUsingStatementes();

        public override string Generate()
        {
            var builder = CSharpCodeGeneratorBuilder.Instance;

            return CSharpCodeGenerator
                .AddFromGenerators(this, builder.TopLevelGenerators)
                .AppendLine("}")
                .AppendLine()
                .ToString();
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
                //try
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
                //catch (Exception)
                { }
            }
        }

        #endregion
    }
}
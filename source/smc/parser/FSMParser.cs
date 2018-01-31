namespace SMC.parser
{
    using System;
    using System.IO;
    using System.Linq;
    using SMC.Builder;
    using SMC.Generator.CSharp;
    using SMC.parser.iface;

    public class FSMParser : SMParserInterface
    {
        #region Fields

        private readonly FSMBuilder builder;
        private readonly string fileName;
        private readonly ParserErrorManager errorManager = new ParserErrorManager();

        #endregion

        #region Constructors & Destructors

        public FSMParser(FSMBuilder theBuilder, string theFileName)
        {
            this.builder = theBuilder;
            this.fileName = theFileName;
            this.FSMGeneratorName = typeof(SMCSharpGenerator).FullName;
            this.builder.ErrorManager = this.errorManager;
        }

        #endregion

        #region Public Properties

        public string FSMGeneratorName { get; set; }

        #endregion

        #region Public Methods

        void SMParserInterface.setFSMGenerator(string s) => this.FSMGeneratorName = s;

        public void setFSMName(string s) => this.builder.SetName(s);

        public void setContextName(string s) => this.builder.SetContextName(s);

        public void setException(string s) => this.builder.SetException(s);

        public void setInitialState(string s) => this.builder.SetInitialState(s);

        public void setVersion(string s) => this.builder.SetVersion(s);

        public void addPragma(string s) => this.builder.AddPragma(s);

        public void addSuperSubState(string theName, string theSuperState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddSuperSubState(theName, theSuperState, l);
        }

        public void addSuperState(string theName, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddSuperState(theName, l);
        }

        public void addSubState(string theName, string theSuperState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddSubState(theName, theSuperState, l);
        }

        public void addState(string theName, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddState(theName, l);
        }

        public void addTransition(string theEvent, string theNextState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddTransition(theEvent, theNextState, l);
        }

        public void addInternalTransition(string theEvent, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.builder.AddInternalTransition(theEvent, l);
        }

        public void addAction(string theAction, int theLineNumber) => this.builder.AddAction(theAction);

        public void addEntryAction(string theAction, int theLineNumber) => this.builder.AddEntryAction(theAction);

        public void addExitAction(string theAction, int theLineNumber) => this.builder.AddExitAction(theAction);

        public void syntaxError(int theLineNumber, string theMessage)
        {
            var l = new ParserSyntaxLocation(this.fileName, theLineNumber);
            this.errorManager.Error(l, theMessage);
        }

        public void processFSM()
        {
        }

        public bool Parse()
        {
            var status = false;
            var ifile = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
            var parser = new SMParser(ifile);
            try
            {
                parser.parseFSM(this);
                status = this.builder.Build();
            }
            catch (ParseException pe)
            {
                throw new Exception(PrintSyntaxError(pe), pe);
            }

            if (this.errorManager.Errors.Any())
            {
                throw new Exception(
                    $"There were errors parsing file {this.fileName}:\n{string.Join("\n\t", this.errorManager.Errors)}");
            }

            return status;
        }

        #endregion

        #region Methods

        private string PrintSyntaxError(ParseException pe)
        {
            var result = new System.Text.StringBuilder();
            var prevLine = pe.currentToken.beginLine;
            var errLine = pe.currentToken.next.beginLine;

            result.AppendLine($"Syntax Error: {this.fileName} line {errLine}");

            if (prevLine != errLine)
            {
                result.AppendLine($"   Unknown field \"{pe.currentToken.next.image}\" in header");
            }
            else
            {
                result.AppendLine($"   Field \"{pe.currentToken.image}\" has invalid value \"{pe.currentToken.next.image}\"");
            }

            return result.ToString();
        }

        #endregion
    }
}
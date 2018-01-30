namespace SMC.parser
{
    using System.IO;
    using SMC.Builder;
    using SMC.parser.iface;


    public class FSMParser : SMParserInterface
    {
        private FSMBuilder itsBuilder;
        private string itsFSMGeneratorName;
        private string itsFileName;
        private ParserErrorManager itsErrorManager = new ParserErrorManager();

        public FSMParser(FSMBuilder theBuilder, string theFileName)
        {
            this.itsBuilder = theBuilder;
            this.itsFileName = theFileName;
            this.itsFSMGeneratorName = "";
            this.itsBuilder.ErrorManager = this.itsErrorManager;
        }

        public void setFSMGenerator(string s)
        { this.itsFSMGeneratorName = s; }

        public string getFSMGeneratorName()
        { return this.itsFSMGeneratorName; }

        public void setFSMName(string s)
        { this.itsBuilder.SetName(s); }

        public void setContextName(string s)
        { this.itsBuilder.SetContextName(s); }

        public void setException(string s)
        { this.itsBuilder.SetException(s); }

        public void setInitialState(string s)
        { this.itsBuilder.SetInitialState(s); }

        public void setVersion(string s)
        { this.itsBuilder.SetVersion(s); }

        public void addPragma(string s)
        { this.itsBuilder.AddPragma(s); }

        public void addSuperSubState(string theName, string theSuperState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddSuperSubState(theName, theSuperState, l);
        }

        public void addSuperState(string theName, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddSuperState(theName, l);
        }

        public void addSubState(string theName, string theSuperState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddSubState(theName, theSuperState, l);
        }

        public void addState(string theName, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddState(theName, l);
        }

        public void addTransition(string theEvent, string theNextState, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddTransition(theEvent, theNextState, l);
        }

        public void addInternalTransition(string theEvent, int theLineNumber)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsBuilder.AddInternalTransition(theEvent, l);
        }

        public void addAction(string theAction, int theLineNumber)
        { this.itsBuilder.AddAction(theAction); }

        public void addEntryAction(string theAction, int theLineNumber)
        { this.itsBuilder.AddEntryAction(theAction); }

        public void addExitAction(string theAction, int theLineNumber)
        { this.itsBuilder.AddExitAction(theAction); }

        public void syntaxError(int theLineNumber, string theMessage)
        {
            var l = new ParserSyntaxLocation(this.itsFileName, theLineNumber);
            this.itsErrorManager.Error(l, theMessage);
        }

        public void processFSM()
        { }

        public bool parse()
        {
            var status = false;
            var ifile = new FileStream(this.itsFileName, FileMode.Open, FileAccess.Read);
            var parser = new SMParser(ifile);
            try
            {
                parser.parseFSM(this);
                status = this.itsBuilder.Build();
            }
            catch (ParseException pe)
            {
                throw new System.InvalidOperationException(printSyntaxError(pe));
            }
            return status;
        }

        private string printSyntaxError(ParseException pe)
        {
            var result = new System.Text.StringBuilder();
            var prevLine = pe.currentToken.beginLine;
            var errLine = pe.currentToken.next.beginLine;

            result.AppendLine("Syntax Error: " + this.itsFileName + " line " + errLine);

            if (prevLine != errLine)
            {
                result.AppendLine("   Unknown field \"" +
                                pe.currentToken.next.image + "\" in header");
            }
            else
            {
                result.AppendLine("   Field \"" + pe.currentToken.image +
                  "\" has invalid value \"" + pe.currentToken.next.image + "\"");
            }

            return result.ToString();
        }
    }
}